#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

///根据keyword的开关去控制该属性的显示和隐藏,keyword都是ON，则开启GUI显示
/*-------------------Shader中的例子-----------------------------
[Toggle] _A("A",float) = 1
[Toggle] _B("B",float) = 1
[ShowProperty(_A_ON,_B_ON)] _Show("Show Property",float) = 1
---------------------------------------------------------------*/
public class ShowPropertyDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr;
    bool _condition;
    public ShowPropertyDrawer(string n1)
    {
        _propertyNameArr = new string[] { n1 };
    }
    public ShowPropertyDrawer(string n1, string n2)
    {
        _propertyNameArr = new string[] { n1, n2 };
    }
    public ShowPropertyDrawer(string n1, string n2, string n3)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
    }
    public ShowPropertyDrawer(string n1, string n2, string n3, string n4)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;
        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }
        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;

            for (int j = 0; j < _propertyNameArr.Length; j++)
            {
                _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
            }
        }
        if (!_condition) editor.DefaultShaderProperty(prop, label);
    }
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }
}
///根据keyword的开关去控制该属性的显示和隐藏,keyword都是ON，则开启GUI隐藏
/*-------------------Shader中的例子-----------------------------
[Toggle] _A("A",float) = 1
[Toggle] _B("B",float) = 1
[HideProperty(_A_ON,_B_ON)] _Show("Show Property",float) = 1
---------------------------------------------------------------*/
public class HidePropertyDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr;
    bool _condition;
    public HidePropertyDrawer(string n1)
    {
        _propertyNameArr = new string[] { n1 };
    }
    public HidePropertyDrawer(string n1, string n2)
    {
        _propertyNameArr = new string[] { n1, n2 };
    }
    public HidePropertyDrawer(string n1, string n2, string n3)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
    }
    public HidePropertyDrawer(string n1, string n2, string n3, string n4)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;
        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }
        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;

            for (int j = 0; j < _propertyNameArr.Length; j++)
            {
                _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
            }
        }
        if (_condition) editor.DefaultShaderProperty(prop, label);
    }
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return _condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }
}
public class ShowToggleDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr;
    bool _condition;
    public ShowToggleDrawer(string n1)
    {
        _propertyNameArr = new string[] { n1 };
    }
    public ShowToggleDrawer(string n1, string n2)
    {
        _propertyNameArr = new string[] { n1, n2 };
    }
    public ShowToggleDrawer(string n1, string n2, string n3)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
    }
    public ShowToggleDrawer(string n1, string n2, string n3, string n4)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
    }
    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;
        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }
        for (int i = 0; i < editor.targets.Length; i++)
        {
            //material object that we're targetting...
            Material mat = editor.targets[i] as Material;
            if (mat != null)
            {
                for (int j = 0; j < _propertyNameArr.Length; j++)
                {
                    _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
                }
            }
        }
        if (!_condition)
        {
            //editor.DefaultShaderProperty(prop, label);
            bool value = (prop.floatValue != 0.0f);
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            value = EditorGUI.Toggle(position, label, value);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = value ? 1.0f : 0.0f;
            }
        }
    }
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }
}
public class ShowSliderDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr;
    bool _condition;

    public ShowSliderDrawer(string n1)
    {
        _propertyNameArr = new string[] { n1 };
    }
    public ShowSliderDrawer(string n1, string n2)
    {
        _propertyNameArr = new string[] { n1, n2 };
    }
    public ShowSliderDrawer(string n1, string n2, string n3)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
    }
    public ShowSliderDrawer(string n1, string n2, string n3, string n4)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
    }
    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;

        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }

        for (int i = 0; i < editor.targets.Length; i++)
        {
            //material object that we're targetting...
            Material mat = editor.targets[i] as Material;
            if (mat != null)
            {
                for (int j = 0; j < _propertyNameArr.Length; j++)
                {
                    _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
                }
            }
        }
        if (!_condition)
        {
            float value = prop.floatValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            string newLabel = label + "[" + prop.rangeLimits.x + "-" + prop.rangeLimits.y + "]";
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

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }
}
//////ShowPowerSlider//////////END
public class ShowIntRangeDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr;
    bool _condition;

    public ShowIntRangeDrawer(string n1)
    {
        _propertyNameArr = new string[] { n1 };
    }
    public ShowIntRangeDrawer(string n1, string n2)
    {
        _propertyNameArr = new string[] { n1, n2 };
    }
    public ShowIntRangeDrawer(string n1, string n2, string n3)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
    }
    public ShowIntRangeDrawer(string n1, string n2, string n3, string n4)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
    }
    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;

        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }

        for (int i = 0; i < editor.targets.Length; i++)
        {
            //material object that we're targetting...
            Material mat = editor.targets[i] as Material;
            if (mat != null)
            {
                for (int j = 0; j < _propertyNameArr.Length; j++)
                {
                    _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
                }
            }
        }
        if (!_condition)
        {
            int value = (int)prop.floatValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;

            string newLabel = label + "[" + (int)prop.rangeLimits.x + "-" + (int)prop.rangeLimits.y + "]";
            float offsetX = newLabel.Length * 10;
            if (offsetX > position.width / 2.0f) offsetX = position.width / 2;

            Rect newLabelPos = new Rect(position.x, position.y, offsetX, position.height);
            EditorGUI.LabelField(position, newLabel);

            Rect newPos = new Rect(position.x + position.width * 0.4f, position.y, position.width * 0.6f, position.height);
            value = EditorGUI.IntSlider(newPos, "", value, (int)prop.rangeLimits.x, (int)prop.rangeLimits.y);

            EditorGUI.showMixedValue = false;


            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = value;
            }
        }
    }

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }
}
public class ShowSpaceDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr = new string[] { };
    bool _condition;
    private float space;
    public ShowSpaceDrawer(string n1, float space)
    {
        _propertyNameArr = new string[] { n1 };
        this.space = space;
    }
    public ShowSpaceDrawer(string n1, string n2, float space)
    {
        _propertyNameArr = new string[] { n1, n2 };
        this.space = space;
    }
    public ShowSpaceDrawer(string n1, string n2, string n3, float space)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        this.space = space;
    }
    public ShowSpaceDrawer(string n1, string n2, string n3, string n4, float space)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        this.space = space;
    }
    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;

        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }

        for (int i = 0; i < editor.targets.Length; i++)
        {
            //material object that we're targetting...
            Material mat = editor.targets[i] as Material;
            if (mat != null)
            {
                for (int j = 0; j < _propertyNameArr.Length; j++)
                {
                    _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
                }
            }
        }
        if (!_condition)
        {
            GUILayout.Space(-16 + space);
            editor.DefaultShaderProperty(prop, label);
        }
    }

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }
}
public class ShowHeaderDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr = new string[] { };
    bool _condition;
    private string info;
    GUIStyle style = new GUIStyle();

    public ShowHeaderDrawer(string n1, string info)
    {
        _propertyNameArr = new string[] { n1 };
        this.info = info;
    }
    public ShowHeaderDrawer(string n1, string n2, string info)
    {
        _propertyNameArr = new string[] { n1, n2 };
        this.info = info;
    }
    public ShowHeaderDrawer(string n1, string n2, string n3, string info)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        this.info = info;
    }
    public ShowHeaderDrawer(string n1, string n2, string n3, string n4, string info)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        this.info = info;
    }
    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;

        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }

        for (int i = 0; i < editor.targets.Length; i++)
        {
            //material object that we're targetting...
            Material mat = editor.targets[i] as Material;
            if (mat != null)
            {
                for (int j = 0; j < _propertyNameArr.Length; j++)
                {
                    _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
                }
            }
        }
        if (!_condition)
        {
            GUILayout.Space(-16);
            style.richText = true;
            GUILayout.Label("<size=12><color=white><b><i>" + info + " </i></b></color></size>", style);
            //EditorGUI.LabelField(position,info,style);
            editor.DefaultShaderProperty(prop, label);
        }
    }

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }
}
public class ShowKeywordEnumDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr = new string[] { };
    string[] _elementArr = new string[] { };
    bool _condition;

    #region 构造器
    //element count 1
    public ShowKeywordEnumDrawer(string n1, float separation, string e1)//params string[] n)
    {
        _propertyNameArr = new string[] { n1 };
        _elementArr = new string[] { e1 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, float separation, string e1)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _elementArr = new string[] { e1 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, float separation, string e1)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _elementArr = new string[] { e1 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, string n4, float separation, string e1)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _elementArr = new string[] { e1 };
    }


    //element count 2
    public ShowKeywordEnumDrawer(string n1, float separation, string e1, string e2)//params string[] n)
    {
        _propertyNameArr = new string[] { n1 };
        _elementArr = new string[] { e1, e2 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, float separation, string e1, string e2)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _elementArr = new string[] { e1, e2 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, float separation, string e1, string e2)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _elementArr = new string[] { e1, e2 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, string n4, float separation, string e1, string e2)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _elementArr = new string[] { e1, e2 };
    }

    //element count 3
    public ShowKeywordEnumDrawer(string n1, float separation, string e1, string e2, string e3)//params string[] n)
    {
        _propertyNameArr = new string[] { n1 };
        _elementArr = new string[] { e1, e2, e3 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, float separation, string e1, string e2, string e3)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _elementArr = new string[] { e1, e2, e3 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, float separation, string e1, string e2, string e3)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _elementArr = new string[] { e1, e2, e3 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, string n4, float separation, string e1, string e2, string e3)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _elementArr = new string[] { e1, e2, e3 };
    }

    //element count 4
    public ShowKeywordEnumDrawer(string n1, float separation, string e1, string e2, string e3, string e4)//params string[] n)
    {
        _propertyNameArr = new string[] { n1 };
        _elementArr = new string[] { e1, e2, e3, e4 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, float separation, string e1, string e2, string e3, string e4)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _elementArr = new string[] { e1, e2, e3, e4 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, float separation, string e1, string e2, string e3, string e4)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _elementArr = new string[] { e1, e2, e3, e4 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, string n4, float separation, string e1, string e2, string e3, string e4)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _elementArr = new string[] { e1, e2, e3, e4 };
    }

    //element count 5
    public ShowKeywordEnumDrawer(string n1, float separation, string e1, string e2, string e3, string e4, string e5)//params string[] n)
    {
        _propertyNameArr = new string[] { n1 };
        _elementArr = new string[] { e1, e2, e3, e4, e5 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, float separation, string e1, string e2, string e3, string e4, string e5)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _elementArr = new string[] { e1, e2, e3, e4, e5 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, float separation, string e1, string e2, string e3, string e4, string e5)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _elementArr = new string[] { e1, e2, e3, e4, e5 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, string n4, float separation, string e1, string e2, string e3, string e4, string e5)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _elementArr = new string[] { e1, e2, e3, e4, e5 };
    }


    //element count 6
    public ShowKeywordEnumDrawer(string n1, float separation, string e1, string e2, string e3, string e4, string e5, string e6)//params string[] n)
    {
        _propertyNameArr = new string[] { n1 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, float separation, string e1, string e2, string e3, string e4, string e5, string e6)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, float separation, string e1, string e2, string e3, string e4, string e5, string e6)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, string n4, float separation, string e1, string e2, string e3, string e4, string e5, string e6)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6 };
    }

    //element count 7
    public ShowKeywordEnumDrawer(string n1, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7)//params string[] n)
    {
        _propertyNameArr = new string[] { n1 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, string n4, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7 };
    }


    //element count 8
    public ShowKeywordEnumDrawer(string n1, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8)//params string[] n)
    {
        _propertyNameArr = new string[] { n1 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, string n4, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8 };
    }

    //element count 9
    public ShowKeywordEnumDrawer(string n1, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8, string e9)//params string[] n)
    {
        _propertyNameArr = new string[] { n1 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8, e9 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8, string e9)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8, e9 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8, string e9)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8, e9 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, string n4, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8, string e9)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8, e9 };
    }

    //element count 10
    public ShowKeywordEnumDrawer(string n1, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8, string e9, string e10)//params string[] n)
    {
        _propertyNameArr = new string[] { n1 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8, string e9, string e10)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8, string e9, string e10)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10 };
    }
    public ShowKeywordEnumDrawer(string n1, string n2, string n3, string n4, float separation, string e1, string e2, string e3, string e4, string e5, string e6, string e7, string e8, string e9, string e10)//params string[] n)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _elementArr = new string[] { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10 };
    }

    #endregion

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;
        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }

        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat != null)
            {
                for (int j = 0; j < _propertyNameArr.Length; j++)
                {
                    _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
                }
            }
        }
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

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }

}
//这个有点乱，无所谓了。。。不行合并写了。。
public class ShowEnumDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr;
    bool _condition;
    string[] _e;
    string[] _blendModes = new string[]
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
    string[] _cullModes = new string[]
    {
        "Off",
        "Front",
        "Back",
    };
    string[] _compareFunction = new string[]
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
    Dictionary<string, int> _dic;
    string _current_dickey;
    bool type;

    #region 构造器
    public ShowEnumDrawer(string n1, string e)
    {
        _propertyNameArr = new string[] { n1 };
        e = e.ToLower();
        switch (e)
        {
            case "unityengine.rendering.blendmode":
                _e = _blendModes;
                type = true;
                break;
            case "unityengine.rendering.cullmode":
                _e = _cullModes;
                type = true;
                break;
            case "unityengine.rendering.comparefunction":
                _e = _compareFunction;
                type = true;
                break;
            default:
                type = false;
                break;
        }
    }
    public ShowEnumDrawer(string n1, string n2, string e)
    {
        _propertyNameArr = new string[] { n1, n2 };
        e = e.ToLower();
        switch (e)
        {
            case "unityengine.rendering.blendmode":
                _e = _blendModes;
                type = true;
                break;
            case "unityengine.rendering.cullmode":
                _e = _cullModes;
                type = true;
                break;
            case "unityengine.rendering.comparefunction":
                _e = _compareFunction;
                type = true;
                break;
            default:
                type = false;
                break;
        }
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        e = e.ToLower();
        switch (e)
        {
            case "unityengine.rendering.blendmode":
                _e = _blendModes;
                type = true;
                break;
            case "unityengine.rendering.cullmode":
                _e = _cullModes;
                type = true;
                break;
            case "unityengine.rendering.comparefunction":
                _e = _compareFunction;
                type = true;
                break;
            default:
                type = false;
                break;
        }
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        e = e.ToLower();
        switch (e)
        {
            case "unityengine.rendering.blendmode":
                _e = _blendModes;
                type = true;
                break;
            case "unityengine.rendering.cullmode":
                _e = _cullModes;
                type = true;
                break;
            case "unityengine.rendering.comparefunction":
                _e = _compareFunction;
                type = true;
                break;
            default:
                type = false;
                break;
        }
    }
    #endregion
    #region 构造器
    //Enum element 1
    public ShowEnumDrawer(string n1, string e1, float f1)
    {
        _propertyNameArr = new string[] { n1 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string e1, float f1)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e1, float f1)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e1, float f1)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        _current_dickey = e1;
    }

    //Enum element 2
    public ShowEnumDrawer(string n1, string e1, float f1, string e2, float f2)
    {
        _propertyNameArr = new string[] { n1 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string e1, float f1, string e2, float f2)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e1, float f1, string e2, float f2)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e1, float f1, string e2, float f2)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        _current_dickey = e1;
    }
    //Enum element 3
    public ShowEnumDrawer(string n1, string e1, float f1, string e2, float f2, string e3, float f3)
    {
        _propertyNameArr = new string[] { n1 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string e1, float f1, string e2, float f2, string e3, float f3)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e1, float f1, string e2, float f2, string e3, float f3)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e1, float f1, string e2, float f2, string e3, float f3)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        _current_dickey = e1;
    }
    //Enum element 4
    public ShowEnumDrawer(string n1, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4)
    {
        _propertyNameArr = new string[] { n1 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        _current_dickey = e1;
    }
    //Enum element 5
    public ShowEnumDrawer(string n1, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5)
    {
        _propertyNameArr = new string[] { n1 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        _current_dickey = e1;
    }
    //Enum element 6
    public ShowEnumDrawer(string n1, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6)
    {
        _propertyNameArr = new string[] { n1 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        _current_dickey = e1;
    }
    //Enum element 7
    public ShowEnumDrawer(string n1, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7)
    {
        _propertyNameArr = new string[] { n1 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        _current_dickey = e1;
    }
    //Enum element 8
    public ShowEnumDrawer(string n1, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8)
    {
        _propertyNameArr = new string[] { n1 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        _current_dickey = e1;
    }
    //Enum element 9
    public ShowEnumDrawer(string n1, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8, string e9, float f9)
    {
        _propertyNameArr = new string[] { n1 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        if (!_dic.ContainsKey(e9)) _dic.Add(e9, (int)f9);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8, string e9, float f9)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        if (!_dic.ContainsKey(e9)) _dic.Add(e9, (int)f9);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8, string e9, float f9)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        if (!_dic.ContainsKey(e9)) _dic.Add(e9, (int)f9);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8, string e9, float f9)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        if (!_dic.ContainsKey(e9)) _dic.Add(e9, (int)f9);
        _current_dickey = e1;
    }
    //Enum element 10
    public ShowEnumDrawer(string n1, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8, string e9, float f9, string e10, float f10)
    {
        _propertyNameArr = new string[] { n1 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        if (!_dic.ContainsKey(e9)) _dic.Add(e9, (int)f9);
        if (!_dic.ContainsKey(e10)) _dic.Add(e10, (int)f10);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8, string e9, float f9, string e10, float f10)
    {
        _propertyNameArr = new string[] { n1, n2 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        if (!_dic.ContainsKey(e9)) _dic.Add(e9, (int)f9);
        if (!_dic.ContainsKey(e10)) _dic.Add(e10, (int)f10);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8, string e9, float f9, string e10, float f10)
    {
        _propertyNameArr = new string[] { n1, n2, n3 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        if (!_dic.ContainsKey(e9)) _dic.Add(e9, (int)f9);
        if (!_dic.ContainsKey(e10)) _dic.Add(e10, (int)f10);
        _current_dickey = e1;
    }
    public ShowEnumDrawer(string n1, string n2, string n3, string n4, string e1, float f1, string e2, float f2, string e3, float f3, string e4, float f4, string e5, float f5, string e6, float f6, string e7, float f7, string e8, float f8, string e9, float f9, string e10, float f10)
    {
        _propertyNameArr = new string[] { n1, n2, n3, n4 };
        _dic = new Dictionary<string, int>();
        if (!_dic.ContainsKey(e1)) _dic.Add(e1, (int)f1);
        if (!_dic.ContainsKey(e2)) _dic.Add(e2, (int)f2);
        if (!_dic.ContainsKey(e3)) _dic.Add(e3, (int)f3);
        if (!_dic.ContainsKey(e4)) _dic.Add(e4, (int)f4);
        if (!_dic.ContainsKey(e5)) _dic.Add(e5, (int)f5);
        if (!_dic.ContainsKey(e6)) _dic.Add(e6, (int)f6);
        if (!_dic.ContainsKey(e7)) _dic.Add(e7, (int)f7);
        if (!_dic.ContainsKey(e8)) _dic.Add(e8, (int)f8);
        if (!_dic.ContainsKey(e9)) _dic.Add(e9, (int)f9);
        if (!_dic.ContainsKey(e10)) _dic.Add(e10, (int)f10);
        _current_dickey = e1;
    }
    #endregion

    void HandleDicSelect(object param)
    {
        object[] param_arr = (param as object[]);
        _current_dickey = (string)param_arr[0];
        MaterialProperty prop = (MaterialProperty)param_arr[1];
        prop.floatValue = _dic[_current_dickey];
    }
    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;
        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }
        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;

            for (int j = 0; j < _propertyNameArr.Length; j++)
            {
                _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
            }
        }
        if (!_condition)
        {
            if (type)
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
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }
}


///下面的只写了一种构造器，按照自己的需要自己添加吧
public class ShowBasedPropertyDrawer : MaterialPropertyDrawer
{
    string _propertyName;
    bool _condition;
    int type = 0;
    float _value;
    Color c = new Color();
    public ShowBasedPropertyDrawer(string n1, float f1)
    {
        _propertyName = n1;
        _value = f1;
        type = 1;
    }
    public ShowBasedPropertyDrawer(string n1)
    {
        _propertyName = n1;
        type = 2;
    }
    public ShowBasedPropertyDrawer(string n1, float r, float g, float b, float a)
    {
        _propertyName = n1;
        this.c = new Color(r, g, b, a);
        type = 3;
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;
        if (_propertyName.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }
        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;
            switch (type)
            {
                case 1: _condition = mat.GetFloat(_propertyName) < _value; break;
                case 2: _condition = mat.GetTexture(_propertyName) == null; break;
                    //case 3: _condition = mat.GetColor(_propertyName).r == c.r;break;//编辑器会报错
            }


        }
        if (!_condition)
        {
            editor.DefaultShaderProperty(prop, label);
        }
    }
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }
}

public class ShowHelpBoxDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr;
    bool _condition;
    string _info;
    MessageType type = MessageType.Error;

    public ShowHelpBoxDrawer(string n1, string info, float t)
    {
        _propertyNameArr = new string[] { n1 };
        this._info = info;
        switch ((int)t)
        {
            case 0: type = MessageType.Error; break;
            case 1: type = MessageType.Info; break;
            case 2: type = MessageType.None; break;
            case 3: type = MessageType.Warning; break;
            default: break;
        }
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;
        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }
        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;

            for (int j = 0; j < _propertyNameArr.Length; j++)
            {
                _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
            }
        }
        if (!_condition)
        {
            EditorGUI.HelpBox(position, _info, type);

            editor.DefaultShaderProperty(prop, label);
        }
    }
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) : 0;
    }
}

public class ShowTextureDrawer : MaterialPropertyDrawer
{
    string[] _propertyNameArr;
    bool _condition;
    Texture tex;
    int height = 0;
    Rect newPos = new Rect();
    Material mat;

    public ShowTextureDrawer(string n1)
    {
        _propertyNameArr = new string[] { n1 };
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        _condition = false;
        if (_propertyNameArr.Length == 0)
        {
            editor.DefaultShaderProperty(prop, label);
            return;
        }
        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat == null) return;

            for (int j = 0; j < _propertyNameArr.Length; j++)
            {
                _condition |= !mat.IsKeywordEnabled(_propertyNameArr[j]);
            }
            tex = mat.GetTexture(prop.name);
            this.mat = mat;
        }
        if (!_condition)
        {

            editor.DefaultShaderProperty(prop, label);

            if (tex != null)
            {
                GUILayout.BeginHorizontal();
                
                newPos.x = position.x-120;
                newPos.y = position.y;
                newPos.width = position.width;
                newPos.height = position.height;
                EditorGUI.DrawTextureAlpha(newPos, tex, ScaleMode.ScaleToFit);

                newPos.x = position.x - 20;
                EditorGUI.DrawTextureTransparent(newPos, tex, ScaleMode.ScaleToFit);

                newPos.x = position.x + 80;
                EditorGUI.DrawPreviewTexture(newPos, tex,mat, ScaleMode.ScaleToFit);
                
                GUILayout.EndHorizontal();
                height = 1;
            }
            else
            {
                height = 0;
            }
        }
    }
    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return !_condition ? base.GetPropertyHeight(prop, label, editor) + 70 * height : 0;
    }
}
#endif
