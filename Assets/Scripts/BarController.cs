using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {
    const float MaxValue = 1;
    const float MinValue = 0;
    [SerializeField] GameObject barRoot;
    [SerializeField] GameObject sliderPrefab;
    GameObject _bar;
    int _pointer;
    GameObject _sliderRoot;

    Slider[] _sliders;

    public float Value {
        get {
            if (_sliders.Length == 0) {
                return 0;
            }

            var result = _pointer * MaxValue;
            if (_pointer != _sliders.Length) {
                result += GetSlider(_pointer).value;
            }

            return result;
        }
        set {
            if (_sliders.Length == 0) {
                return;
            }

            if (value < _sliders.Length * MaxValue) {
                Debug.Log($"Set {value}");
            }

            if (value < 0) {
                value = 0;
            } else if (value > _sliders.Length * MaxValue) {
                value = _sliders.Length * MaxValue;
            }

            if (value < _sliders.Length * MaxValue) {
                Debug.Log($"Set now {value}");
            }

            var fullSliders = (int) (value / MaxValue);
            for (var i = 0; i < fullSliders; i++) {
                GetSlider(i).value = MaxValue;
            }

            _pointer = fullSliders;

            if (value < _sliders.Length * MaxValue) {
                Debug.Log($"pointer: {_pointer}");
            }

            if (_pointer == _sliders.Length) {
                return;
            }

            var lastSliderValue = value % MaxValue;
            if (value < _sliders.Length * MaxValue) {
                Debug.Log($"lst: {lastSliderValue}");
            }

            GetSlider(_pointer).value = lastSliderValue;

            for (var i = _pointer + 1; i < _sliders.Length; i++) {
                GetSlider(i).value = 0;
            }
        }
    }

    void OnDestroy() {
        if (_bar) {
            Destroy(_bar);
        }
    }

    Slider GetSlider(int index) {
        return _sliders[_sliders.Length - index - 1];
    }

    public void SetHUDRoot(GameObject hudRoot) {
        _bar = Instantiate(barRoot, hudRoot.transform);
        _sliderRoot = Enumerable
            .Range(0, _bar.transform.childCount)
            .Select(i => _bar.transform.GetChild(i).gameObject)
            .First(child => child.CompareTag("SliderRoot"));
    }

    public void SetSize(int barNumber) {
        ClearRoot();

        _sliders = Enumerable.Range(0, barNumber)
            .Select(_ => Instantiate(sliderPrefab, _sliderRoot.transform))
            .Select(slider => slider.GetComponent<Slider>())
            .ToArray();
        _pointer = 0;

        foreach (var slider in _sliders) {
            slider.minValue = MinValue;
            slider.maxValue = MaxValue;
        }
    }

    void ClearRoot() {
        var childCount = _sliderRoot.transform.childCount;
        for (var i = 0; i < childCount; i++) {
            Destroy(_sliderRoot.transform.GetChild(i).gameObject);
        }
    }
}
