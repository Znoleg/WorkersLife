using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.Gui
{
    public class DayImagePresenter : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _imageCanvasGroup;
        [SerializeField] private float _pureNightAlpha = 0.95f;
        
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
            
            float differenceFromNight = Mathf.Abs(pureDayValue - DayTimeConfig.NightValue);
            float alphaValue = MapValue(differenceFromNight, DayTimeConfig.NightValue, 0f, 0f, _pureNightAlpha);
            _imageCanvasGroup.alpha = alphaValue;
        }
        
        private float MapValue(float x, float xLeft, float xRight, float resLeft, float resRight)
        {
            if (xLeft == xRight)
            {
                return resLeft;
            }
            
            return (x - xLeft) / (xRight - xLeft) * (resRight - resLeft) + resLeft;
        }
    }
}
