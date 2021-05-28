using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {
    const float MaxValue = 1;
    const float MinValue = 0;
    [SerializeField] GameObject rootPrefab;
    [SerializeField] GameObject barPrefab;
    GameObject _barRoot;
    int _pointer;
    GameObject _root;

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

            if (value < 0) {
                value = 0;
            } else if (value > _sliders.Length * MaxValue) {
                value = _sliders.Length * MaxValue;
            }

            var fullSliders = (int) (value / MaxValue);
            for (var i = 0; i < fullSliders; i++) {
                GetSlider(i).value = MaxValue;
            }

            _pointer = fullSliders;

            if (_pointer == _sliders.Length) {
                return;
            }

            var lastSliderValue = value % MaxValue;
            GetSlider(_pointer).value = lastSliderValue;

            for (var i = _pointer + 1; i < _sliders.Length; i++) {
                GetSlider(i).value = 0;
            }
        }
    }

    void OnDestroy() {
        if (_root) {
            Destroy(_root);
        }
    }

    Slider GetSlider(int index) {
        return _sliders[_sliders.Length - index - 1];
    }

    public void SetHUDRoot(GameObject hudRoot) {
        _root = Instantiate(rootPrefab, hudRoot.transform);
        _barRoot = Enumerable
            .Range(0, _root.transform.childCount)
            .Select(i => _root.transform.GetChild(i).gameObject)
            .First(child => child.CompareTag("BarRoot"));
    }

    public void SetSize(int barNumber) {
        ClearRoot();

        _sliders = Enumerable.Range(0, barNumber)
            .Select(_ => Instantiate(barPrefab, _barRoot.transform))
            .Select(slider => slider.GetComponent<Slider>())
            .ToArray();
        _pointer = 0;

        foreach (var slider in _sliders) {
            slider.minValue = MinValue;
            slider.maxValue = MaxValue;
        }
    }

    void ClearRoot() {
        var childCount = _barRoot.transform.childCount;
        for (var i = 0; i < childCount; i++) {
            Destroy(_barRoot.transform.GetChild(i).gameObject);
        }
    }
}
