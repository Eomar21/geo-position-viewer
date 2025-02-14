using GeoPositionViewer.Models;
using GeoPositionViewer.Models.Extensions;
using GeoPositionViewer.Services;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace GeoPositionViewer.App.ViewModels
{
    public class PositionViewModel : ViewModelBase, IDisposable
    {
        private GeoAveragePosition m_GeoAveragePosition;
        private GeoPosition m_GeoPosition;
        private GeoPosition m_GeoRawPosition;
        private readonly CompositeDisposable m_Disposable = new();

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
            m_GeoPosition = GeoPosition.Empty;
            m_GeoAveragePosition = GeoAveragePosition.Empty;
            m_GeoRawPosition = GeoPosition.Empty;
            var positionGeneratedStream = Observable.FromEventPattern<GeoPosition>(
                h => positionSimulator.PositionGenerated += h,
                h => positionSimulator.PositionGenerated -= h)
                .Select(e => e.EventArgs);

            positionGeneratedStream.Subscribe(x =>
            {
                GeoRawPosition = x.RoundValue(m_RoundingDigits);
            }).DisposeWith(m_Disposable);
            var throttledStream = positionGeneratedStream.Sample(TimeSpan.FromSeconds(m_ThrottleSeconds));
            throttledStream.ObserveOn(RxApp.MainThreadScheduler).Subscribe(x =>
            {
                GeoPosition = x.RoundValue(m_RoundingDigits);
                m_Positions.Add(x.Position.RoundValue(m_RoundingDigits));
                if (m_Positions is not null)
                {
                    GeoAveragePosition = geoPositionProcessor.GetAveragePosition(m_Positions).RoundValue(m_RoundingDigits);
                }
            }).DisposeWith(m_Disposable);
        }

        public void Dispose()
        {
            m_Disposable.Dispose();
        }
    }
}
