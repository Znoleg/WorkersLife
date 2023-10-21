using Main.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.Gui
{
    public class DayTimeSliderPresenter : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        private DayTimeData _dayTimeData;

        public void Init(DayTimeData dayTimeData)
        {
            _dayTimeData = dayTimeData;
        }

        private void Update()
        {
            if (_dayTimeData == null) return;
            float pureDayValue = _dayTimeData.DayValue;
            if (_dayTimeData.DayValue > 1f)
            {
                pureDayValue %= (int) _dayTimeData.DayValue;
            }

            _slider.value = pureDayValue;
        }
    }
}