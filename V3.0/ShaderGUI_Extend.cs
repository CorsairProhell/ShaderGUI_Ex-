#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Deployment.Internal;
using System.Security.Policy;

//最下面有个案例shader
public sealed class Show : MaterialPropertyDrawer
{
    private string[] _propertyArr;
    private bool _condition;
    private int _offset;
    private string _nFun;
    private bool _orAnd = true;
    private float _spaceValue;
    private string[] tmp;
    private string _info;
    GUIStyle style = new GUIStyle();
    private string[] _elementArr;
    MessageType type = MessageType.Error;

    private string[] _e;
    private string[] _blendModes = new string[]
    {
        "Zero",
        "One",
        "DstColor",
        "SrcColor",
        "OneMinusDstColor",
        "SrcAlpha",
        "OneMinusSrcColor",
        "DstAlpha",
        "OneMinusDstAlpha",
        "SrcAlphaSaturate",
        "OneMinusSrcAlpha",
    };
    private string[] _cullModes = new string[]
    {
        "Off",
        "Front",
        "Back",
    };
    private string[] _compareFunction = new string[]
    {
        "Disabled",
        "Never",
        "Less",
        "Equal",
        "LessEqual",
        "Greater",
        "NotEqual",
        "GreaterEqual",
        "Always",
    };
    private Dictionary<string, int> _dic;
    private string _current_dickey;
    private bool _typeEnum;

    public Show() { }
    public Show(string fn, string o1)
    {
        tmp = fn.Split('#'); _nFun = tmp[0]; if (tmp.Length == 2) _info = tmp[1];
        _propertyArr = new string[] { o1 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2)
    {
        tmp = fn.Split('#'); _nFun = tmp[0]; if (tmp.Length == 2) _info = tmp[1];
        _propertyArr = new string[] { o1, o2 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, string o3)
    {
        tmp = fn.Split('#'); _nFun = tmp[0]; if (tmp.Length == 2) _info = tmp[1];
        _propertyArr = new string[] { o1, o2, o3 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, string o3, string o4)
    {
        tmp = fn.Split('#'); _nFun = tmp[0]; if (tmp.Length == 2) _info = tmp[1];
        _propertyArr = new string[] { o1, o2, o3, o4 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    //Space
    public Show(string fn, float space, string o1)
    {
        _nFun = fn;
        _spaceValue = space;
        _propertyArr = new string[] { o1 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, float space, string o1, string o2)
    {
        _nFun = fn;
        _spaceValue = space;
        _propertyArr = new string[] { o1, o2 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, float space, string o1, string o2, string o3)
    {
        _nFun = fn;
        _spaceValue = space;
        _propertyArr = new string[] { o1, o2, o3 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, float space, string o1, string o2, string o3, string o4)
    {
        _nFun = fn;
        _spaceValue = space;
        _propertyArr = new string[] { o1, o2, o3, o4 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    //KeywordEnum  和部分Enum   ----separation没有意义只是为了防止构造器无法重载
    public Show(string fn, string o1, float separation, string e)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1 };
        if (fn == "Enum" || fn == "EnumOr")
        {
            SwitchType(e);
        }
        else
        {
            _elementArr = e.Split('#');
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, float separation, string e)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2 };
        if (fn == "Enum" || fn == "EnumOr")
        {
            SwitchType(e);
        }
        else
        {
            _elementArr = e.Split('#');
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, string o3, float separation, string e)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3 };
        if (fn == "Enum" || fn == "EnumOr")
        {
            SwitchType(e);
        }
        else
        {
            _elementArr = e.Split('#');
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, string o3, string o4, float separation, string e)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3, o4 };
        if (fn == "Enum" || fn == "EnumOr")
        {
            SwitchType(e);
        }
        else
        {
            _elementArr = e.Split('#');
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    //Enum
    public Show(string fn, string o1, float separation, string e, string f)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1 };

        string[] eArr = e.Split('#');
        string[] fStrArr = f.Split('#');
        _dic = new Dictionary<string, int>();

        for (int i = 0; i < fStrArr.Length; i++)
        {
            if (!_dic.ContainsKey(eArr[i])) _dic.Add(eArr[i], (int)Convert.ToSingle(fStrArr[i]));
            _current_dickey = eArr[i];
        }

        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, float separation, string e, string f)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2 };

        string[] eArr = e.Split('#');
        string[] fStrArr = f.Split('#');
        _dic = new Dictionary<string, int>();

        for (int i = 0; i < fStrArr.Length; i++)
        {
            if (!_dic.ContainsKey(eArr[i])) _dic.Add(eArr[i], (int)Convert.ToSingle(fStrArr[i]));
            _current_dickey = eArr[i];
        }

        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, string o3, float separation, string e, string f)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3 };

        string[] eArr = e.Split('#');
        string[] fStrArr = f.Split('#');
        _dic = new Dictionary<string, int>();

        for (int i = 0; i < fStrArr.Length; i++)
        {
            if (!_dic.ContainsKey(eArr[i])) _dic.Add(eArr[i], (int)Convert.ToSingle(fStrArr[i]));
            _current_dickey = eArr[i];
        }

        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, string o3, string o4, float separation, string e, string f)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3, o4 };

        string[] eArr = e.Split('#');
        string[] fStrArr = f.Split('#');
        _dic = new Dictionary<string, int>();

        for (int i = 0; i < fStrArr.Length; i++)
        {
            if (!_dic.ContainsKey(eArr[i])) _dic.Add(eArr[i], (int)Convert.ToSingle(fStrArr[i]));
            _current_dickey = eArr[i];
        }

        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    //HelpBox
    public Show(string fn, string o1, string info, float t)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1 };
        this._info = info;
        switch ((int)t)
        {
            case 0: type = MessageType.Error; break;
            case 1: type = MessageType.Info; break;
            case 2: type = MessageType.None; break;
            case 3: type = MessageType.Warning; break;
            default: break;
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, string info, float t)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2 };
        this._info = info;
        switch ((int)t)
        {
            case 0: type = MessageType.Error; break;
            case 1: type = MessageType.Info; break;
            case 2: type = MessageType.None; break;
            case 3: type = MessageType.Warning; break;
            default: break;
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, string o3, string info, float t)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3 };
        this._info = info;
        switch ((int)t)
        {
            case 0: type = MessageType.Error; break;
            case 1: type = MessageType.Info; break;
            case 2: type = MessageType.None; break;
            case 3: type = MessageType.Warning; break;
            default: break;
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Show(string fn, string o1, string o2, string o3, string o4, string info, float t)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3, o4 };
        this._info = info;
        switch ((int)t)
        {
            case 0: type = MessageType.Error; break;
            case 1: type = MessageType.Info; break;
            case 2: type = MessageType.None; break;
            case 3: type = MessageType.Warning; break;
            default: break;
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_orAnd;
        GetType().GetMethod(_nFun).Invoke(this, new object[] { position, prop, label, editor });
    }


    #region Texture
    //------------------------------Texture------------------------------
    public void InitTexture() { _offset = 18; _orAnd = true; }
    public void Texture(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }

    public void InitTextureOr() { _offset = 18; _orAnd = false; }
    public void TextureOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }
    //------------------------------Texture------------------------------
    #endregion
    #region Toggle
    //------------------------------Toggle------------------------------
    public void InitToggle() { _offset = 0; _orAnd = true; }
    public void Toggle(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawTogglePropDefault(position, prop, label, editor);
    }

    public void InitToggleOr() { _offset = 0; _orAnd = false; }
    public void ToggleOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawTogglePropDefault(position, prop, label, editor);
    }
    //------------------------------Toggle------------------------------
    #endregion
    #region Slider
    //------------------------------Slider------------------------------

    public void InitSlider() { _offset = 0; _orAnd = true; }
    public void Slider(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSliderPropDefault(position, prop, label, editor);
    }

    public void InitSliderOr() { _offset = 0; _orAnd = false; }
    public void SliderOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSliderPropDefault(position, prop, label, editor);
    }

    public void InitRange() { _offset = 0; _orAnd = true; }
    public void Range(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSliderPropDefault(position, prop, label, editor);
    }

    public void InitRangeOr() { _offset = 0; _orAnd = false; }
    public void RangeOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSliderPropDefault(position, prop, label, editor);
    }
    //------------------------------Slider------------------------------
    #endregion
    #region IntSlider
    //------------------------------IntSlider------------------------------

    public void InitIntSlider() { _offset = 0; _orAnd = true; }
    public void IntSlider(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawIntSliderPropDefault(position, prop, label, editor);
    }

    public void InitIntSliderOr() { _offset = 0; _orAnd = false; }
    public void IntSliderOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawIntSliderPropDefault(position, prop, label, editor);
    }

    public void InitIntRange() { _offset = 0; _orAnd = true; }
    public void IntRange(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawIntSliderPropDefault(position, prop, label, editor);
    }

    public void InitIntRangeOr() { _offset = 0; _orAnd = false; }
    public void IntRangeOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawIntSliderPropDefault(position, prop, label, editor);
    }
    //------------------------------IntSlider------------------------------
    #endregion
    #region PowSlider 卒
    //////ShowPowerSlider//////////卒
    #endregion
    #region Space
    //------------------------------Space------------------------------
    public void InitSpace() { _offset = 0; _orAnd = true; }
    public void Space(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSpacePropDefault(position, prop, label, editor);
    }

    public void InitSpaceOr() { _offset = 0; _orAnd = false; }
    public void SpaceOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSpacePropDefault(position, prop, label, editor);
    }
    //------------------------------Space------------------------------
    #endregion
    #region Header
    //------------------------------Header------------------------------
    public void InitHeader() { _offset = 0; _orAnd = true; }
    public void Header(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawHeaderPropDefault(position, prop, label, editor);
    }

    public void InitHeaderOr() { _offset = 0; _orAnd = false; }
    public void HeaderOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawHeaderPropDefault(position, prop, label, editor);
    }
    //------------------------------Header------------------------------
    #endregion
    #region KeywordEnum
    //------------------------------KeywordEnum------------------------------
    public void InitKeywordEnum() { _offset = 0; _orAnd = true; }
    public void KeywordEnum(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawKeywordEnumPropDefault(position, prop, label, editor);
    }

    public void InitKeywordEnumOr() { _offset = 0; _orAnd = false; }
    public void KeywordEnumOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawKeywordEnumPropDefault(position, prop, label, editor);
    }
    //------------------------------KeywordEnum------------------------------
    #endregion
    #region Enum
    //------------------------------Enum------------------------------
    public void InitEnum() { _offset = 0; _orAnd = true; }
    public void Enum(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawEnumPropDefault(position, prop, label, editor);
    }

    public void InitEnumOr() { _offset = 0; _orAnd = false; }
    public void EnumOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawEnumPropDefault(position, prop, label, editor);
    }
    //------------------------------Enum------------------------------
    #endregion
    #region HelpBox
    //------------------------------HelpBox------------------------------
    public void InitHelpBox() { _offset = 0; _orAnd = true; }
    public void HelpBox(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawHelpBoxPropDefault(position, prop, label, editor);
    }

    public void InitHelpBoxOr() { _offset = 0; _orAnd = false; }
    public void HelpBoxOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawHelpBoxPropDefault(position, prop, label, editor);
    }
    //------------------------------HelpBox------------------------------
    #endregion

    //Texture Toggle Slider IntSlider
    public void DrawProp(MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (_propertyArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }
        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;
            for (int j = 0; j < _propertyArr.Length; j++)
            {
                if (_orAnd)
                    _condition |= !mat.IsKeywordEnabled(_propertyArr[j].ToString());
                else
                    _condition &= !mat.IsKeywordEnabled(_propertyArr[j].ToString());
            }
        }
    }
    //Texture
    public void DrawPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (!_condition) editor.DefaultShaderProperty(prop, label);
    }
    //Texture Toggle Slider IntSlider
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) - _offset : 0;
    }


    //Toggle
    public void DrawTogglePropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (!_condition)
        {
            bool value = prop.floatValue != 0.0f;
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            value = EditorGUI.Toggle(position, label, value);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck()) prop.floatValue = value ? 1.0f : 0.0f;
        }
    }

    //Slider
    public void DrawSliderPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (!_condition)
        {
            float value = prop.floatValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            string newLabel = label + "  [" + prop.rangeLimits.x + "-" + prop.rangeLimits.y + "]";
            float offsetX = newLabel.Length * 10;
            if (offsetX > position.width / 2.0f) offsetX = position.width / 2;

            Rect newLabelPos = new Rect(position.x, position.y, offsetX, position.height);
            EditorGUI.LabelField(position, newLabel);

            Rect newPos = new Rect(position.x + position.width * 0.4f, position.y, position.width * 0.6f, position.height);
            value = EditorGUI.Slider(newPos, "", value, prop.rangeLimits.x, prop.rangeLimits.y);

            //value = EditorGUI.Slider(position, label, value, prop.rangeLimits.x, prop.rangeLimits.y);

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = value;
            }
        }
    }

    //IntSlider
    public void DrawIntSliderPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (!_condition)
        {
            int value = Mathf.FloorToInt(prop.floatValue);

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            string newLabel = label + "  [" + Mathf.FloorToInt(prop.rangeLimits.x) + "-" + Mathf.FloorToInt(prop.rangeLimits.y) + "]";
            float offsetX = newLabel.Length * 10;
            if (offsetX > position.width / 2.0f) offsetX = position.width / 2;

            Rect newLabelPos = new Rect(position.x, position.y, offsetX, position.height);
            EditorGUI.LabelField(position, newLabel);

            Rect newPos = new Rect(position.x + position.width * 0.4f, position.y, position.width * 0.6f, position.height);
            value = EditorGUI.IntSlider(newPos, "", value, Mathf.FloorToInt(prop.rangeLimits.x), Mathf.FloorToInt(prop.rangeLimits.y));

            //value = EditorGUI.Slider(position, label, value, prop.rangeLimits.x, prop.rangeLimits.y);

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = value;
            }
        }
    }

    //Space
    public void DrawSpacePropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (!_condition)
        {
            GUILayout.Space(-16 + _spaceValue);
            editor.DefaultShaderProperty(prop, label);
        }
    }

    //Header DrawHeaderPropDefault
    public void DrawHeaderPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (!_condition)
        {
            GUILayout.Space(-16);
            style.richText = true;
            GUILayout.Label("<size=12><color=white><b><i>" + _info + " </i></b></color></size>", style);
            //EditorGUI.LabelField(position,info,style);
            editor.DefaultShaderProperty(prop, label);
        }
    }

    //DrawKeywordEnumPropDefault
    public void DrawKeywordEnumPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (!_condition)
        {
            int index = (int)prop.floatValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;

            index = EditorGUI.Popup(position, label, index, _elementArr);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = index;
            }
        }
    }

    //Enum
    private void SwitchType(string e)
    {
        e = e.ToLower();
        switch (e)
        {
            case "unityengine.rendering.blendmode":
                _e = _blendModes;
                _typeEnum = true;
                break;
            case "unityengine.rendering.cullmode":
                _e = _cullModes;
                _typeEnum = true;
                break;
            case "unityengine.rendering.comparefunction":
                _e = _compareFunction;
                _typeEnum = true;
                break;
            default:
                _typeEnum = false;
                break;
        }
    }
    private void HandleDicSelect(object param)
    {
        object[] param_arr = (param as object[]);
        _current_dickey = (string)param_arr[0];
        MaterialProperty prop = (MaterialProperty)param_arr[1];
        prop.floatValue = _dic[_current_dickey];
    }
    public void DrawEnumPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (!_condition)
        {
            if (_typeEnum)
            {
                int index = (int)prop.floatValue;
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = prop.hasMixedValue;

                index = EditorGUI.Popup(position, label, index, _e);

                EditorGUI.showMixedValue = false;
                if (EditorGUI.EndChangeCheck())
                {
                    prop.floatValue = index;
                }
            }
            else
            {
                GUILayout.Space(-16);
                GUILayout.BeginHorizontal();
                GUILayout.Label(label);
                if (GUILayout.Button(_current_dickey, GUILayout.Width(65), GUILayout.Height(16)))
                {
                    GenericMenu menu = new GenericMenu();
                    List<string> dkey = new List<string>(_dic.Keys);
                    dkey.Sort();
                    for (int i = 0; i < dkey.Count; i++)
                    {
                        menu.AddItem(new GUIContent(dkey[i]), _current_dickey == dkey[i], HandleDicSelect, new object[] { dkey[i], prop });
                    }
                    menu.ShowAsContext();
                }
                GUILayout.EndHorizontal();
            }
        }
    }

    //DrawHelpBoxPropDefault
    public void DrawHelpBoxPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (!_condition)
        {
            EditorGUI.HelpBox(position, _info, type);
            editor.DefaultShaderProperty(prop, label);
        }
    }

}
public sealed class ShowBased : MaterialPropertyDrawer
{
    private string[] _propertyArr;
    private float[] _propertyValueArr;
    private bool _condition;
    private int _offset;
    private string _nFun;
    private bool _orAnd = true;
    private string[] _symbolArr;
    private Color c = new Color();

    //Texture
    public ShowBased(string fn, string o1)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public ShowBased(string fn, string o1, string o2)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public ShowBased(string fn, string o1, string o2, string o3)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public ShowBased(string fn, string o1, string o2, string o3, string o4)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3, o4 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    //Float
    public ShowBased(string fn, string o1, string s1, float f1)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1 };
        _symbolArr = new string[] { s1 };
        _propertyValueArr = new float[] { f1 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public ShowBased(string fn, string o1, string s1, float f1, string o2, string s2, float f2)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2 };
        _symbolArr = new string[] { s1, s2 };
        _propertyValueArr = new float[] { f1, f2 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public ShowBased(string fn, string o1, string s1, float f1, string o2, string s2, float f2, string o3, string s3, float f3)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3 };
        _symbolArr = new string[] { s1, s2, s3 };
        _propertyValueArr = new float[] { f1, f2, f3 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public ShowBased(string fn, string o1, string s1, float f1, string o2, string s2, float f2, string o3, string s3, float f3, string o4, string s4, float f4)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3, o4 };
        _symbolArr = new string[] { s1, s2, s3, s4 };
        _propertyValueArr = new float[] { f1, f2, f3, f4 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    //Color
    //public ShowBased(string fn, string o1,float r,float g,float b,float a)
    //{
    //    _nFun = fn;
    //    _propertyArr = new string[] { o1 };
    //    c = new Color(r, g, b, a);
    //    GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    //}

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_orAnd;
        GetType().GetMethod(_nFun).Invoke(this, new object[] { position, prop, label, editor });
    }



    #region Texture
    //------------------------------Texture------------------------------
    public void InitTexture() { _offset = 18; _orAnd = true; }
    public void Texture(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawTextureProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }

    public void InitTextureOr() { _offset = 18; _orAnd = false; }
    public void TextureOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawTextureProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }
    //------------------------------Texture------------------------------
    #endregion
    #region Float
    //------------------------------Float------------------------------
    public void InitFloat() { _offset = 18; _orAnd = true; }
    public void Float(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawFloatProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }

    public void InitFloatOr() { _offset = 18; _orAnd = false; }
    public void FloatOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawFloatProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }
    //------------------------------Float------------------------------
    #endregion


    //Texture
    public void DrawTextureProp(MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (CheckProp(prop, label, editor) == null) return;

        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;

            bool _tmpBool = mat.GetTexture(_propertyArr[0]) == null;
            for (int j = 1; j < _propertyArr.Length; j++)
            {
                if (_orAnd)
                    _tmpBool |= mat.GetTexture(_propertyArr[j]) == null;
                else
                    _tmpBool &= mat.GetTexture(_propertyArr[j]) == null;
            }
            _condition = _tmpBool;
        }
    }

    //Float
    public void DrawFloatProp(MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (CheckProp(prop, label, editor) == null) return;

        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;

            bool _tmpBool = CompareTwoNum(mat.GetFloat(_propertyArr[0]), _symbolArr[0], _propertyValueArr[0]);
            for (int j = 1; j < _propertyArr.Length; j++)
            {
                if (_orAnd)
                    _tmpBool |= CompareTwoNum(mat.GetFloat(_propertyArr[j]), _symbolArr[j], _propertyValueArr[j]);
                else
                    _tmpBool &= CompareTwoNum(mat.GetFloat(_propertyArr[j]), _symbolArr[j], _propertyValueArr[j]);
            }
            _condition = _tmpBool;
        }
    }

    //Color

    private bool CompareTwoNum(float f1, string symbol, float f2)
    {
        switch (symbol)
        {
            case "Less": return f1 >= f2;
            case "LEqual": return f1 > f2;
            case "Greater": return f1 <= f2;
            case "GEqual": return f1 < f2;
            case "NotEqual": return f1 == f2;
            case "Equal": return f1 != f2;
        }
        return false;
    }

    public void DrawPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (!_condition) editor.DefaultShaderProperty(prop, label);
    }

    //Texture Toggle Slider IntSlider
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) - _offset : 0;
    }
    private object CheckProp(MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (_propertyArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return null;
        }
        return 1;
    }

}

//跟上面基本一样，但是不想在重新做了
public sealed class Hide : MaterialPropertyDrawer
{
    private string[] _propertyArr;
    private bool _condition;
    private int _offset;
    private string _nFun;
    private bool _orAnd = true;
    private float _spaceValue;
    private string[] tmp;
    private string _info;
    GUIStyle style = new GUIStyle();
    private string[] _elementArr;
    MessageType type = MessageType.Error;

    private string[] _e;
    private string[] _blendModes = new string[]
    {
        "Zero",
        "One",
        "DstColor",
        "SrcColor",
        "OneMinusDstColor",
        "SrcAlpha",
        "OneMinusSrcColor",
        "DstAlpha",
        "OneMinusDstAlpha",
        "SrcAlphaSaturate",
        "OneMinusSrcAlpha",
    };
    private string[] _cullModes = new string[]
    {
        "Off",
        "Front",
        "Back",
    };
    private string[] _compareFunction = new string[]
    {
        "Disabled",
        "Never",
        "Less",
        "Equal",
        "LessEqual",
        "Greater",
        "NotEqual",
        "GreaterEqual",
        "Always",
    };
    private Dictionary<string, int> _dic;
    private string _current_dickey;
    private bool _typeEnum;

    public Hide() { }
    public Hide(string fn, string o1)
    {
        tmp = fn.Split('#'); _nFun = tmp[0]; if (tmp.Length == 2) _info = tmp[1];
        _propertyArr = new string[] { o1 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2)
    {
        tmp = fn.Split('#'); _nFun = tmp[0]; if (tmp.Length == 2) _info = tmp[1];
        _propertyArr = new string[] { o1, o2 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, string o3)
    {
        tmp = fn.Split('#'); _nFun = tmp[0]; if (tmp.Length == 2) _info = tmp[1];
        _propertyArr = new string[] { o1, o2, o3 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, string o3, string o4)
    {
        tmp = fn.Split('#'); _nFun = tmp[0]; if (tmp.Length == 2) _info = tmp[1];
        _propertyArr = new string[] { o1, o2, o3, o4 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    //Space
    public Hide(string fn, float space, string o1)
    {
        _nFun = fn;
        _spaceValue = space;
        _propertyArr = new string[] { o1 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, float space, string o1, string o2)
    {
        _nFun = fn;
        _spaceValue = space;
        _propertyArr = new string[] { o1, o2 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, float space, string o1, string o2, string o3)
    {
        _nFun = fn;
        _spaceValue = space;
        _propertyArr = new string[] { o1, o2, o3 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, float space, string o1, string o2, string o3, string o4)
    {
        _nFun = fn;
        _spaceValue = space;
        _propertyArr = new string[] { o1, o2, o3, o4 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    //KeywordEnum  和部分Enum   ----separation没有意义只是为了防止构造器无法重载
    public Hide(string fn, string o1, float separation, string e)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1 };
        if (fn == "Enum" || fn == "EnumOr")
        {
            SwitchType(e);
        }
        else
        {
            _elementArr = e.Split('#');
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, float separation, string e)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2 };
        if (fn == "Enum" || fn == "EnumOr")
        {
            SwitchType(e);
        }
        else
        {
            _elementArr = e.Split('#');
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, string o3, float separation, string e)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3 };
        if (fn == "Enum" || fn == "EnumOr")
        {
            SwitchType(e);
        }
        else
        {
            _elementArr = e.Split('#');
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, string o3, string o4, float separation, string e)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3, o4 };
        if (fn == "Enum" || fn == "EnumOr")
        {
            SwitchType(e);
        }
        else
        {
            _elementArr = e.Split('#');
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    //Enum
    public Hide(string fn, string o1, float separation, string e, string f)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1 };

        string[] eArr = e.Split('#');
        string[] fStrArr = f.Split('#');
        _dic = new Dictionary<string, int>();

        for (int i = 0; i < fStrArr.Length; i++)
        {
            if (!_dic.ContainsKey(eArr[i])) _dic.Add(eArr[i], (int)Convert.ToSingle(fStrArr[i]));
            _current_dickey = eArr[i];
        }

        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, float separation, string e, string f)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2 };

        string[] eArr = e.Split('#');
        string[] fStrArr = f.Split('#');
        _dic = new Dictionary<string, int>();

        for (int i = 0; i < fStrArr.Length; i++)
        {
            if (!_dic.ContainsKey(eArr[i])) _dic.Add(eArr[i], (int)Convert.ToSingle(fStrArr[i]));
            _current_dickey = eArr[i];
        }

        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, string o3, float separation, string e, string f)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3 };

        string[] eArr = e.Split('#');
        string[] fStrArr = f.Split('#');
        _dic = new Dictionary<string, int>();

        for (int i = 0; i < fStrArr.Length; i++)
        {
            if (!_dic.ContainsKey(eArr[i])) _dic.Add(eArr[i], (int)Convert.ToSingle(fStrArr[i]));
            _current_dickey = eArr[i];
        }

        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, string o3, string o4, float separation, string e, string f)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3, o4 };

        string[] eArr = e.Split('#');
        string[] fStrArr = f.Split('#');
        _dic = new Dictionary<string, int>();

        for (int i = 0; i < fStrArr.Length; i++)
        {
            if (!_dic.ContainsKey(eArr[i])) _dic.Add(eArr[i], (int)Convert.ToSingle(fStrArr[i]));
            _current_dickey = eArr[i];
        }

        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    //HelpBox
    public Hide(string fn, string o1, string info, float t)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1 };
        this._info = info;
        switch ((int)t)
        {
            case 0: type = MessageType.Error; break;
            case 1: type = MessageType.Info; break;
            case 2: type = MessageType.None; break;
            case 3: type = MessageType.Warning; break;
            default: break;
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, string info, float t)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2 };
        this._info = info;
        switch ((int)t)
        {
            case 0: type = MessageType.Error; break;
            case 1: type = MessageType.Info; break;
            case 2: type = MessageType.None; break;
            case 3: type = MessageType.Warning; break;
            default: break;
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, string o3, string info, float t)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3 };
        this._info = info;
        switch ((int)t)
        {
            case 0: type = MessageType.Error; break;
            case 1: type = MessageType.Info; break;
            case 2: type = MessageType.None; break;
            case 3: type = MessageType.Warning; break;
            default: break;
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public Hide(string fn, string o1, string o2, string o3, string o4, string info, float t)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3, o4 };
        this._info = info;
        switch ((int)t)
        {
            case 0: type = MessageType.Error; break;
            case 1: type = MessageType.Info; break;
            case 2: type = MessageType.None; break;
            case 3: type = MessageType.Warning; break;
            default: break;
        }
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_orAnd;
        GetType().GetMethod(_nFun).Invoke(this, new object[] { position, prop, label, editor });
    }


    #region Texture
    //------------------------------Texture------------------------------
    public void InitTexture() { _offset = 18; _orAnd = true; }
    public void Texture(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }

    public void InitTextureOr() { _offset = 18; _orAnd = false; }
    public void TextureOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }
    //------------------------------Texture------------------------------
    #endregion
    #region Toggle
    //------------------------------Toggle------------------------------
    public void InitToggle() { _offset = 0; _orAnd = true; }
    public void Toggle(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawTogglePropDefault(position, prop, label, editor);
    }

    public void InitToggleOr() { _offset = 0; _orAnd = false; }
    public void ToggleOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawTogglePropDefault(position, prop, label, editor);
    }
    //------------------------------Toggle------------------------------
    #endregion
    #region Slider
    //------------------------------Slider------------------------------

    public void InitSlider() { _offset = 0; _orAnd = true; }
    public void Slider(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSliderPropDefault(position, prop, label, editor);
    }

    public void InitSliderOr() { _offset = 0; _orAnd = false; }
    public void SliderOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSliderPropDefault(position, prop, label, editor);
    }

    public void InitRange() { _offset = 0; _orAnd = true; }
    public void Range(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSliderPropDefault(position, prop, label, editor);
    }

    public void InitRangeOr() { _offset = 0; _orAnd = false; }
    public void RangeOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSliderPropDefault(position, prop, label, editor);
    }
    //------------------------------Slider------------------------------
    #endregion
    #region IntSlider
    //------------------------------IntSlider------------------------------

    public void InitIntSlider() { _offset = 0; _orAnd = true; }
    public void IntSlider(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawIntSliderPropDefault(position, prop, label, editor);
    }

    public void InitIntSliderOr() { _offset = 0; _orAnd = false; }
    public void IntSliderOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawIntSliderPropDefault(position, prop, label, editor);
    }

    public void InitIntRange() { _offset = 0; _orAnd = true; }
    public void IntRange(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawIntSliderPropDefault(position, prop, label, editor);
    }

    public void InitIntRangeOr() { _offset = 0; _orAnd = false; }
    public void IntRangeOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawIntSliderPropDefault(position, prop, label, editor);
    }
    //------------------------------IntSlider------------------------------
    #endregion
    #region PowSlider 卒
    //////ShowPowerSlider//////////卒
    #endregion
    #region Space
    //------------------------------Space------------------------------
    public void InitSpace() { _offset = 0; _orAnd = true; }
    public void Space(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSpacePropDefault(position, prop, label, editor);
    }

    public void InitSpaceOr() { _offset = 0; _orAnd = false; }
    public void SpaceOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawSpacePropDefault(position, prop, label, editor);
    }
    //------------------------------Space------------------------------
    #endregion
    #region Header
    //------------------------------Header------------------------------
    public void InitHeader() { _offset = 0; _orAnd = true; }
    public void Header(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawHeaderPropDefault(position, prop, label, editor);
    }

    public void InitHeaderOr() { _offset = 0; _orAnd = false; }
    public void HeaderOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawHeaderPropDefault(position, prop, label, editor);
    }
    //------------------------------Header------------------------------
    #endregion
    #region KeywordEnum
    //------------------------------KeywordEnum------------------------------
    public void InitKeywordEnum() { _offset = 0; _orAnd = true; }
    public void KeywordEnum(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawKeywordEnumPropDefault(position, prop, label, editor);
    }

    public void InitKeywordEnumOr() { _offset = 0; _orAnd = false; }
    public void KeywordEnumOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawKeywordEnumPropDefault(position, prop, label, editor);
    }
    //------------------------------KeywordEnum------------------------------
    #endregion
    #region Enum
    //------------------------------Enum------------------------------
    public void InitEnum() { _offset = 0; _orAnd = true; }
    public void Enum(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawEnumPropDefault(position, prop, label, editor);
    }

    public void InitEnumOr() { _offset = 0; _orAnd = false; }
    public void EnumOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawEnumPropDefault(position, prop, label, editor);
    }
    //------------------------------Enum------------------------------
    #endregion
    #region HelpBox
    //------------------------------HelpBox------------------------------
    public void InitHelpBox() { _offset = 0; _orAnd = true; }
    public void HelpBox(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawHelpBoxPropDefault(position, prop, label, editor);
    }

    public void InitHelpBoxOr() { _offset = 0; _orAnd = false; }
    public void HelpBoxOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawProp(prop, label, editor);
        DrawHelpBoxPropDefault(position, prop, label, editor);
    }
    //------------------------------HelpBox------------------------------
    #endregion

    //Texture Toggle Slider IntSlider
    public void DrawProp(MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (_propertyArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }
        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;
            for (int j = 0; j < _propertyArr.Length; j++)
            {
                if (_orAnd)
                    _condition |= !mat.IsKeywordEnabled(_propertyArr[j].ToString());
                else
                    _condition &= !mat.IsKeywordEnabled(_propertyArr[j].ToString());
            }
        }
    }
    //Texture
    public void DrawPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_condition;
        if (!_condition)
        {
            editor.DefaultShaderProperty(prop, label);
        }
        _condition = !_condition;
    }
    //Texture Toggle Slider IntSlider
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) - _offset : 0;
    }


    //Toggle
    public void DrawTogglePropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_condition;
        if (!_condition)
        {
            bool value = prop.floatValue != 0.0f;
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            value = EditorGUI.Toggle(position, label, value);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck()) prop.floatValue = value ? 1.0f : 0.0f;
        }
        _condition = !_condition;
    }

    //Slider
    public void DrawSliderPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_condition;
        if (!_condition)
        {
            float value = prop.floatValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            string newLabel = label + "  [" + prop.rangeLimits.x + "-" + prop.rangeLimits.y + "]";
            float offsetX = newLabel.Length * 10;
            if (offsetX > position.width / 2.0f) offsetX = position.width / 2;

            Rect newLabelPos = new Rect(position.x, position.y, offsetX, position.height);
            EditorGUI.LabelField(position, newLabel);

            Rect newPos = new Rect(position.x + position.width * 0.4f, position.y, position.width * 0.6f, position.height);
            value = EditorGUI.Slider(newPos, "", value, prop.rangeLimits.x, prop.rangeLimits.y);

            //value = EditorGUI.Slider(position, label, value, prop.rangeLimits.x, prop.rangeLimits.y);

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = value;
            }
        }
        _condition = !_condition;
    }

    //IntSlider
    public void DrawIntSliderPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_condition;
        if (!_condition)
        {
            int value = Mathf.FloorToInt(prop.floatValue);

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            string newLabel = label + "  [" + Mathf.FloorToInt(prop.rangeLimits.x) + "-" + Mathf.FloorToInt(prop.rangeLimits.y) + "]";
            float offsetX = newLabel.Length * 10;
            if (offsetX > position.width / 2.0f) offsetX = position.width / 2;

            Rect newLabelPos = new Rect(position.x, position.y, offsetX, position.height);
            EditorGUI.LabelField(position, newLabel);

            Rect newPos = new Rect(position.x + position.width * 0.4f, position.y, position.width * 0.6f, position.height);
            value = EditorGUI.IntSlider(newPos, "", value, Mathf.FloorToInt(prop.rangeLimits.x), Mathf.FloorToInt(prop.rangeLimits.y));

            //value = EditorGUI.Slider(position, label, value, prop.rangeLimits.x, prop.rangeLimits.y);

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = value;
            }
        }
        _condition = !_condition;
    }

    //Space
    public void DrawSpacePropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_condition;
        if (!_condition)
        {
            GUILayout.Space(-16 + _spaceValue);
            editor.DefaultShaderProperty(prop, label);
        }
        _condition = !_condition;
    }

    //Header DrawHeaderPropDefault
    public void DrawHeaderPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_condition;
        if (!_condition)
        {
            GUILayout.Space(-16);
            style.richText = true;
            GUILayout.Label("<size=12><color=white><b><i>" + _info + " </i></b></color></size>", style);
            //EditorGUI.LabelField(position,info,style);
            editor.DefaultShaderProperty(prop, label);
        }
        _condition = !_condition;
    }

    //DrawKeywordEnumPropDefault
    public void DrawKeywordEnumPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_condition;
        if (!_condition)
        {
            int index = (int)prop.floatValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;

            index = EditorGUI.Popup(position, label, index, _elementArr);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = index;
            }
        }
        _condition = !_condition;
    }

    //Enum
    private void SwitchType(string e)
    {
        e = e.ToLower();
        switch (e)
        {
            case "unityengine.rendering.blendmode":
                _e = _blendModes;
                _typeEnum = true;
                break;
            case "unityengine.rendering.cullmode":
                _e = _cullModes;
                _typeEnum = true;
                break;
            case "unityengine.rendering.comparefunction":
                _e = _compareFunction;
                _typeEnum = true;
                break;
            default:
                _typeEnum = false;
                break;
        }
    }
    private void HandleDicSelect(object param)
    {
        object[] param_arr = (param as object[]);
        _current_dickey = (string)param_arr[0];
        MaterialProperty prop = (MaterialProperty)param_arr[1];
        prop.floatValue = _dic[_current_dickey];
    }
    public void DrawEnumPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_condition;
        if (!_condition)
        {
            if (_typeEnum)
            {
                int index = (int)prop.floatValue;
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = prop.hasMixedValue;

                index = EditorGUI.Popup(position, label, index, _e);

                EditorGUI.showMixedValue = false;
                if (EditorGUI.EndChangeCheck())
                {
                    prop.floatValue = index;
                }
            }
            else
            {
                GUILayout.Space(-16);
                GUILayout.BeginHorizontal();
                GUILayout.Label(label);
                if (GUILayout.Button(_current_dickey, GUILayout.Width(65), GUILayout.Height(16)))
                {
                    GenericMenu menu = new GenericMenu();
                    List<string> dkey = new List<string>(_dic.Keys);
                    dkey.Sort();
                    for (int i = 0; i < dkey.Count; i++)
                    {
                        menu.AddItem(new GUIContent(dkey[i]), _current_dickey == dkey[i], HandleDicSelect, new object[] { dkey[i], prop });
                    }
                    menu.ShowAsContext();
                }
                GUILayout.EndHorizontal();
            }
        }
        _condition = !_condition;
    }

    //DrawHelpBoxPropDefault
    public void DrawHelpBoxPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_condition;
        if (!_condition)
        {
            EditorGUI.HelpBox(position, _info, type);
            editor.DefaultShaderProperty(prop, label);
        }
        _condition = !_condition;
    }

}
public sealed class HideBased : MaterialPropertyDrawer
{
    private string[] _propertyArr;
    private float[] _propertyValueArr;
    private bool _condition;
    private int _offset;
    private string _nFun;
    private bool _orAnd = true;
    private string[] _symbolArr;
    private Color c = new Color();

    //Texture
    public HideBased(string fn, string o1)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public HideBased(string fn, string o1, string o2)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public HideBased(string fn, string o1, string o2, string o3)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public HideBased(string fn, string o1, string o2, string o3, string o4)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3, o4 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }

    //Float
    public HideBased(string fn, string o1, string s1, float f1)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1 };
        _symbolArr = new string[] { s1 };
        _propertyValueArr = new float[] { f1 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public HideBased(string fn, string o1, string s1, float f1, string o2, string s2, float f2)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2 };
        _symbolArr = new string[] { s1, s2 };
        _propertyValueArr = new float[] { f1, f2 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public HideBased(string fn, string o1, string s1, float f1, string o2, string s2, float f2, string o3, string s3, float f3)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3 };
        _symbolArr = new string[] { s1, s2, s3 };
        _propertyValueArr = new float[] { f1, f2, f3 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    public HideBased(string fn, string o1, string s1, float f1, string o2, string s2, float f2, string o3, string s3, float f3, string o4, string s4, float f4)
    {
        _nFun = fn;
        _propertyArr = new string[] { o1, o2, o3, o4 };
        _symbolArr = new string[] { s1, s2, s3, s4 };
        _propertyValueArr = new float[] { f1, f2, f3, f4 };
        GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    }
    //Color
    //public HideBased(string fn, string o1,float r,float g,float b,float a)
    //{
    //    _nFun = fn;
    //    _propertyArr = new string[] { o1 };
    //    c = new Color(r, g, b, a);
    //    GetType().GetMethod("Init" + _nFun).Invoke(this, null);
    //}

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_orAnd;
        GetType().GetMethod(_nFun).Invoke(this, new object[] { position, prop, label, editor });
    }



    #region Texture
    //------------------------------Texture------------------------------
    public void InitTexture() { _offset = 18; _orAnd = true; }
    public void Texture(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawTextureProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }

    public void InitTextureOr() { _offset = 18; _orAnd = false; }
    public void TextureOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawTextureProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }
    //------------------------------Texture------------------------------
    #endregion
    #region Float
    //------------------------------Float------------------------------
    public void InitFloat() { _offset = 18; _orAnd = true; }
    public void Float(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawFloatProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }

    public void InitFloatOr() { _offset = 18; _orAnd = false; }
    public void FloatOr(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        DrawFloatProp(prop, label, editor);
        DrawPropDefault(position, prop, label, editor);
    }
    //------------------------------Float------------------------------
    #endregion


    //Texture
    public void DrawTextureProp(MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (CheckProp(prop, label, editor) == null) return;

        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;

            bool _tmpBool = mat.GetTexture(_propertyArr[0]) == null;
            for (int j = 1; j < _propertyArr.Length; j++)
            {
                if (_orAnd)
                    _tmpBool |= mat.GetTexture(_propertyArr[j]) == null;
                else
                    _tmpBool &= mat.GetTexture(_propertyArr[j]) == null;
            }
            _condition = _tmpBool;
        }
    }

    //Float
    public void DrawFloatProp(MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (CheckProp(prop, label, editor) == null) return;

        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;

            bool _tmpBool = CompareTwoNum(mat.GetFloat(_propertyArr[0]), _symbolArr[0], _propertyValueArr[0]);
            for (int j = 1; j < _propertyArr.Length; j++)
            {
                if (_orAnd)
                    _tmpBool |= CompareTwoNum(mat.GetFloat(_propertyArr[j]), _symbolArr[j], _propertyValueArr[j]);
                else
                    _tmpBool &= CompareTwoNum(mat.GetFloat(_propertyArr[j]), _symbolArr[j], _propertyValueArr[j]);
            }
            _condition = _tmpBool;
        }
    }

    //Color

    private bool CompareTwoNum(float f1, string symbol, float f2)
    {
        switch (symbol)
        {
            case "Less": return f1 >= f2;
            case "LEqual": return f1 > f2;
            case "Greater": return f1 <= f2;
            case "GEqual": return f1 < f2;
            case "NotEqual": return f1 == f2;
            case "Equal": return f1 != f2;
        }
        return false;
    }

    public void DrawPropDefault(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = !_condition;
        if (!_condition) editor.DefaultShaderProperty(prop, label);
        _condition = !_condition;
    }

    //Texture Toggle Slider IntSlider
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) - _offset : 0;
    }
    private object CheckProp(MaterialProperty prop, string label, MaterialEditor editor)
    {
        if (_propertyArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return null;
        }
        return 1;
    }

}

public class TextureChannelWindow : EditorWindow
{
    public static TextureChannelWindow win;
    public static int _sel;
    private static Texture2D[] _texAll;
    public static void Open(Texture2D[] texs,float posX,float posY)
    {
        //win = GetWindow<TextureChannelWindow>();
        if (win == null) win = CreateInstance<TextureChannelWindow>();
        //win.ShowPopup();
        var buttonRect = new Rect(100, 100, 300, 100);
        var windowSize = new Vector2(260, 260);
        win.ShowAsDropDown(buttonRect, windowSize);
        win.position = new Rect(win.position) { x = posX, y = posY };
        //win.minSize = new Vector2(260, 260);
        //win.maxSize = new Vector2(261, 261);
        _texAll = texs;
    }
    
    public void OnGUI()
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            {
                GUILayout.BeginHorizontal("box", GUILayout.Width(258));
                {
                    _sel = GUILayout.SelectionGrid(_sel, _texAll, 2, "gridlist", GUILayout.Width(258), GUILayout.Height(256));
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
    }
}
public class ShaderGUI_Extend : EditorWindow
{
    private static ShaderGUI_Extend win;
    private static string text = @"请看ShaderGUI_Extend.Shader中的使用";
    private int _sel = 0;
    private static Texture2D[] texAll;
    private static Material mat;
    private static Shader shader;

    [MenuItem("CONTEXT/Material/ShaderGUI_Extend Help")]
    static void Open(MenuCommand menuCommand)
    {
        win = GetWindow<ShaderGUI_Extend>();
        win.minSize = new Vector2(400, 400);
        win.maxSize = new Vector2(401, 401);
        mat = menuCommand.context as Material;
        shader = mat.shader;
        //Debug.Log(mat);
        //Debug.Log(Selection.activeObject.name);
        LayerTex();

    }


    private void OnGUI()
    {
        GUILayout.TextField(text, GUILayout.Width(400));

        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            {
                GUILayout.BeginHorizontal("box", GUILayout.Width(340));
                {
                    _sel = GUILayout.SelectionGrid(_sel, texAll, 4, "gridlist", GUILayout.Width(364), GUILayout.Height(90));
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Check R G B A", GUILayout.ExpandWidth(true), GUILayout.Height(40)))
            {
                TextureChannelWindow.Open(GetEachChannel(texAll[_sel]), win.position.x+400, win.position.y);
                //mat.SetTexture("_MainTex1", GetEachChannel(texAll[_sel])[0]);
            }
            if (GUILayout.Button("Update", GUILayout.ExpandWidth(true), GUILayout.Height(40)))
            {
                LayerTex();
            }
        }
        GUILayout.EndHorizontal();
    }

    static void LayerTex()
    {
        Transform select = Selection.activeTransform;
        List<Texture2D> allTextures = new List<Texture2D>();
        for (int i = 0; i < ShaderUtil.GetPropertyCount(shader); i++)
        {
            if (ShaderUtil.GetPropertyType(shader, i) == ShaderUtil.ShaderPropertyType.TexEnv)
            {
                //这里因为是getassetpreview，所以透明图如果没有设置Alpha is Transparent为True，看到的alpha会是个1。。。
                Texture2D tex = AssetPreview.GetAssetPreview(mat.GetTexture(ShaderUtil.GetPropertyName(shader, i))) as Texture2D;//获取的是preview的图，尺寸小
                //mat.GetTexture(ShaderUtil.GetPropertyName(shader, i)) as Texture2D;//获取的是原图,但是原图需要Enable Read/Write，尺寸大//
                allTextures.Add(tex);
            }
        }
        texAll = allTextures.ToArray();
    }

    static Texture2D[] GetEachChannel(Texture2D sourceTexture)
    {
        int width = sourceTexture.width;
        int height = sourceTexture.height;
        Color[] colors = sourceTexture.GetPixels();
        Color[] colorsR = new Color[colors.Length];
        Color[] colorsG = new Color[colors.Length];
        Color[] colorsB = new Color[colors.Length];
        Color[] colorsA = new Color[colors.Length];

        Texture2D[] RGBA = new Texture2D[4]
        {
            new Texture2D(width, height,TextureFormat.ARGB32, false),
            new Texture2D(width, height,TextureFormat.ARGB32, false),
            new Texture2D(width, height,TextureFormat.ARGB32, false),
            new Texture2D(width, height,TextureFormat.ARGB32, false)
        };

        for (int i = 0; i < colors.Length; i++)
        {
            colorsR[i] = new Color(colors[i].r, colors[i].r, colors[i].r, 1f);
            colorsG[i] = new Color(colors[i].g, colors[i].g, colors[i].g, 1f);
            colorsB[i] = new Color(colors[i].b, colors[i].b, colors[i].b, 1f);
            colorsA[i] = new Color(colors[i].a, colors[i].a, colors[i].a, 1f);
        }
        RGBA[0].SetPixels(colorsR);
        RGBA[0].Apply();
        RGBA[1].SetPixels(colorsG);
        RGBA[1].Apply();
        RGBA[2].SetPixels(colorsB);
        RGBA[2].Apply();
        RGBA[3].SetPixels(colorsA);
        RGBA[3].Apply();

        return RGBA;
    }

    static bool HasAlpha(Color[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (colors[i].a < 1f)
                return true;
        }
        return false;
    }
}
#endif

//案例shader
/*
Shader "Unlit/ShaderGUI_Extend"
{
	Properties
	{
		//_MainTexT1("Texture T1", 2D) = "white" {}
		//_MainTexT2("Texture T2", 2D) = "white" {}
		//_FloatT1("Float T1",float) = 0.5
		//_FloatT2("Float T2",float) = 0.5
		//_ColorT1("Color T1",color) = (1,1,1,1)
		//_ColorT2("Color T2",color) = (1,1,1,1)
		[Toggle] _A("A", float) = 1
		[Toggle] _B("B", float) = 1


		[Hide(Texture, _A_ON)]_MainTex1("TextureA", 2D) = "white" {}
		[Hide(Texture, _B_ON)]_MainTex2("TextureB", 2D) = "white" {}
		[Hide(Texture, _A_ON, _B_ON)]_MainTex3("TextureAB", 2D) = "white" {}
		
		//[Hide(TextureOr,_A_ON)]_MainTex4("TextureA", 2D) = "white" {}
		//[Hide(TextureOr,_B_ON)]_MainTex5("TextureB", 2D) = "white" {}
		//[Hide(TextureOr,_A_ON,_B_ON)]_MainTex6("TextureAB", 2D) = "white" {}

		//[Show(Toggle,_A_ON)]_Toggle1("ToggleA",float) = 1
		//[Show(Toggle,_B_ON)]_Toggle2("ToggleB",float) = 1
		//[Show(Toggle,_A_ON,_B_ON)]_Toggle3("ToggleAB",float) = 1

		//[Show(ToggleOr,_A_ON)]_Toggle4("ToggleA",float) = 1
		//[Show(ToggleOr,_B_ON)]_Toggle5("ToggleB",float) = 1
		//[Show(ToggleOr,_A_ON,_B_ON)]_Toggle6("ToggleAB",float) = 1

		//[Show(Slider,_A_ON)]_Slider1("SliderA",Range(0,1)) = 1
		//[Show(Slider,_B_ON)]_Slider2("SliderB",Range(0,1)) = 1
		//[Show(Slider,_A_ON,_B_ON)]_Slider3("SliderAB",Range(0,1)) = 1

		//[Show(SliderOr,_A_ON)]_Slider4("SliderA",Range(0,1)) = 1
		//[Show(SliderOr,_B_ON)]_Slider5("SliderB",Range(0,1)) = 1
		//[Show(SliderOr,_A_ON,_B_ON)]_Slider6("SliderAB",Range(0,1)) = 1

		//[Show(IntSlider,_A_ON)]_IntSlider1("IntSliderA",Range(0,1)) = 1
		//[Show(IntSlider,_B_ON)]_IntSlider2("IntSliderB",Range(0,1.1)) = 1
		//[Show(IntSlider,_A_ON,_B_ON)]_IntSlider3("IntSliderAB",Range(-2.2,2.8)) = 1

		//[Show(IntSliderOr,_A_ON)]_IntSlider4("IntSliderA",Range(0,1)) = 1
		//[Show(IntSliderOr,_B_ON)]_IntSlider5("IntSliderB",Range(0,1.1)) = 1
		//[Show(IntSliderOr,_A_ON,_B_ON)]_IntSlider6("IntSliderAB",Range(-2.2,2.8)) = 1

		//[Show(Space,10,_A_ON)]_Space1("SpaceA",Range(0,1)) = 1
		//[Show(Space,10,_B_ON)]_Space2("SpaceB",Range(0,1.1)) = 1
		//[Show(Space,10,_A_ON,_B_ON)]_Space3("SpaceAB",Range(-2.2,2.8)) = 1
		
		//[Show(SpaceOr,10,_A_ON)]_Space4("SpaceA",Range(0,1)) = 1
		//[Show(SpaceOr,10,_B_ON)]_Space5("SpaceB",Range(0,1.1)) = 1
		//[Show(SpaceOr,10,_A_ON,_B_ON)]_Space6("SpaceAB",Range(-2.2,2.8)) = 1

		//[Show(Header#Only Englist,_A_ON)] _Header1("HeaderA",float) = 1
		//[Show(Header#Englist,_B_ON)] _Header2("HeaderB",float) = 1
		//[Show(Header#Englist,_A_ON,_B_ON)] _Header3("HeaderAB",float) = 1

		//[Show(HeaderOr#Only Englist,_A_ON)] _Header1("HeaderA",float) = 1
		//[Show(HeaderOr#Englist,_B_ON)] _Header2("HeaderB",float) = 1
		//[Show(HeaderOr#Englist,_A_ON,_B_ON)] _Header3("HeaderAB",float) = 1

		////----参数中的1没有实际的意义，任何数值都可以，只是作为分隔符使用
		//[Show(KeywordEnum,_A_ON,1, None#Add#Multiply#a#b#c#d#e)] _KeywordTest1("(ShowKeywordEnum)A可开启显示", Float) = 0
		//[Show(KeywordEnum,_B_ON,1, None)] _KeywordTest2("(ShowKeywordEnum)B可开启显示", Float) = 0
		//[Show(KeywordEnum,_A_ON,_B_ON,1, None#Add#Multiply#a#b#c#d#e#f#g)] _KeywordTest3("(ShowKeywordEnum)A与B可开启显示", Float) = 0

		//[Show(KeywordEnumOr,_A_ON,1, None#Add#Multiply#a#b#c#d#e)] _KeywordTest1("(ShowKeywordEnum)A可开启显示", Float) = 0
		//[Show(KeywordEnumOr,_B_ON,1, None)] _KeywordTest2("(ShowKeywordEnum)B可开启显示", Float) = 0
		//[Show(KeywordEnumOr,_A_ON,_B_ON,1, None#Add#Multiply#a#b#c#d#e#f#g)] _KeywordTest3("(ShowKeywordEnum)A与B可开启显示", Float) = 0

		//[Show(Enum,_A_ON,1,UnityEngine.Rendering.BlendMode)] _Blend1("Blend1", Float) = 1
		//[Show(Enum,_A_ON,1,UnityEngine.Rendering.CullMode)] _Blend2("Blend2", Float) = 1
		//[Show(Enum,_A_ON,1,UnityEngine.Rendering.CompareFunction)] _Blend3("Blend3", Float) = 1
		//[Show(Enum,_A_ON,_B_ON,1,UnityEngine.Rendering.BlendMode)] _Blend4("Blend4", Float) = 1
		//[Show(Enum,_A_ON,_B_ON,1,UnityEngine.Rendering.CullMode)] _Blend5("Blend5", Float) = 1
		//[Show(Enum,_A_ON,_B_ON,1,UnityEngine.Rendering.CompareFunction)] _Blend6("Blend6", Float) = 1

		//[Show(EnumOr,_A_ON,1,UnityEngine.Rendering.BlendMode)] _Blend7("Blend7", Float) = 1
		//[Show(EnumOr,_A_ON,1,UnityEngine.Rendering.CullMode)] _Blend8("Blend8", Float) = 1
		//[Show(EnumOr,_A_ON,1,UnityEngine.Rendering.CompareFunction)] _Blend9("Blend9", Float) = 1
		//[Show(EnumOr,_A_ON,_B_ON,1,UnityEngine.Rendering.BlendMode)] _Blend10("Blend10", Float) = 1
		//[Show(EnumOr,_A_ON,_B_ON,1,UnityEngine.Rendering.CullMode)] _Blend11("Blend11", Float) = 1
		//[Show(EnumOr,_A_ON,_B_ON,1,UnityEngine.Rendering.CompareFunction)] _Blend12("Blend12", Float) = 1

		//[Show(Enum,_A_ON,1,One#SrcAlpha,1#5)] _Blend13("Blend13", Float) = 1
		//[Show(Enum,_A_ON,_B_ON,1,One#SrcAlpha#AAAA,1#5#7)] _Blend14("Blend14", Float) = 1

		//[Show(EnumOr,_A_ON,1,One#SrcAlpha,1#5)] _Blend15("Blend15", Float) = 1
		//[Show(EnumOr,_A_ON,_B_ON,1,One#SrcAlpha#AAAA,1#5#7)] _Blend16("Blend16", Float) = 1
		////----参数中的1没有实际的意义，任何数值都可以，只是作为分隔符使用

		//[Show(HelpBox,_A_ON,Waring,0)] _TextField1("Text1",float) = 0
		//[Show(HelpBox,_A_ON,_B_ON,You are a good boy,1)] _TextField2("Text2",float) = 0
		//
		//[Show(HelpBoxOr,_A_ON,Waring,2)] _TextField3("Text1",float) = 0
		//[Show(HelpBoxOr,_A_ON,_B_ON,You are a good boy,3)] _TextField4("Text2",float) = 0

		
		//[HideBased(TextureOr,_MainTexT1)]_MainTexBased1("Texture Based 1", 2D) = "white" {}
		//[HideBased(TextureOr,_MainTexT1,_MainTexT2)]_MainTexBased2("Texture Based 2", 2D) = "white" {}
		
		//[ShowBased(Float,_FloatT1,Less,0.5)]_MainTexBased3("Texture Based 3", 2D) = "white" {}
		//[ShowBased(Float,_FloatT1,Less,0.5,_FloatT2,Less,0.5)]_MainTexBased4("Texture Based 4", 2D) = "white" {}

		//[ShowBased(FloatOr, _FloatT1, Less, 0.5)]_MainTexBased5("Texture Based 5", 2D) = "white" {}
		//[ShowBased(FloatOr, _FloatT1, Less, 0.5, _FloatT2, Less, 0.5)]_MainTexBased6("Texture Based 6", 2D) = "white" {}

		//[HideProperty(_A_ON)]_MainTex3("Texture3", 2D) = "white" {}
		//[HideProperty(_B_ON)]_MainTex4("Texture4", 2D) = "white" {}
		//[HidePropertyOr(_A_ON,_B_ON)]_MainTex5("Texture5", 2D) = "white" {}


	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature _A_ON
            #pragma shader_feature _B_ON

            struct appdata
            {
                float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return 1;
			}
			ENDCG
		}
	}
}
*/
