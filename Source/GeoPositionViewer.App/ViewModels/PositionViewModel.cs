using GeoPositionViewer.Models;
using GeoPositionViewer.Models.Extensions;
using GeoPositionViewer.Services;
using ReactiveUI;
using System.Reactive.Linq;

namespace GeoPositionViewer.App.ViewModels
{
    public class PositionViewModel : ViewModelBase
    {
        private GeoAveragePosition m_GeoAveragePosition;
        private GeoPosition m_GeoPosition;
        private GeoPosition m_GeoRawPosition;

        private List<Position> m_Positions = new List<Position>();
        private const int m_RoundingDigits = 6;
        private const int m_ThrottleSeconds = 2;

        public GeoAveragePosition GeoAveragePosition
        {
            get => m_GeoAveragePosition;
            set => this.RaiseAndSetIfChanged(ref m_GeoAveragePosition, value);
        }
        public GeoPosition GeoPosition
        {
            get => m_GeoPosition;
            set => this.RaiseAndSetIfChanged(ref m_GeoPosition, value);
        }

        public GeoPosition GeoRawPosition
        {
            get => m_GeoRawPosition;
            set => this.RaiseAndSetIfChanged(ref m_GeoRawPosition, value);
        }

        public PositionViewModel(IGeoPositionProcessor geoPositionProcessor, PositionSimulator positionSimulator)
        {
            GeoPosition = GeoPosition.Empty;
            GeoAveragePosition = GeoAveragePosition.Empty;
            var positionGeneratedStream = Observable.FromEventPattern<GeoPosition>(
                h => positionSimulator.PositionGenerated += h,
                h => positionSimulator.PositionGenerated -= h)
                .Select(e => e.EventArgs);

            positionGeneratedStream.Subscribe(x =>
            {
                GeoRawPosition = x.RoundValue(m_RoundingDigits);
            });
            var throttledStream = positionGeneratedStream.Sample(TimeSpan.FromSeconds(m_ThrottleSeconds));
            throttledStream.ObserveOn(RxApp.MainThreadScheduler).Subscribe(x =>
            {
                GeoPosition = x.RoundValue(m_RoundingDigits);
                m_Positions.Add(x.Position.RoundValue(m_RoundingDigits));
                if (m_Positions is not null)
                {
                    GeoAveragePosition = geoPositionProcessor.GetAveragePosition(m_Positions).RoundValue(m_RoundingDigits);
                }
            });
        }
    }
}
