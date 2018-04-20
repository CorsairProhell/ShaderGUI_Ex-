Shader "Unlit/ShaderGUI_Ex"
{
	Properties
	{
		_SlideToggle("Slider Toggle",Range(0,2)) = 1
		[Toggle] _A("A",float) = 1
		[Toggle] _B("B",float) = 1
		////----
		//[ShowProperty(_A_ON)] _MainTex1("(ShowProperty)A可开启显示", 2D) = "white" {}
		//[ShowProperty(_A_ON,_B_ON)] _MainTex2("(ShowProperty)A与B可开启显示", 2D) = "white" {}
		////----
		//[HideProperty(_A_ON)] _MainTex3("(HideProperty)A可关闭显示", 2D) = "white" {}
		//[HideProperty(_A_ON,_B_ON)] _MainTex4("(HideProperty)A与B可关闭显示", 2D) = "white" {}
		////----
		//[ShowToggle(_A_ON)]_C("(ShowToggle)A可开启显示",float) = 1
		//[ShowToggle(_A_ON,_B_ON)]_D("(ShowToggle)A与B可开启显示",float) = 1
		////----
		//[ShowSlider(_A_ON)] _Slider1("(ShowSlider)A可开启显示",Range(0,4)) = 1
		//[ShowSlider(_A_ON,_B_ON)] _Slider2("(ShowSlider)A与B可开启显示",Range(0,4)) = 1
		////----
		//[ShowIntRange(_A_ON)]_Slider3("(ShowSlider)A可开启显示",Range(0,4)) = 1
		//[ShowIntRange(_A_ON,_B_ON)]_Slider4("(ShowSlider)A与B可开启显示",Range(0,4)) = 1
		////----
		//[ShowSpace(_A_ON,10)] _Float1("(ShowSpace)A可开启显示",float) = 1
		//[ShowSpace(_A_ON,_B_ON,10)] _Float2("(ShowSpace)A与B可开启显示",float) = 1
		////----
		//[ShowHeader(_A_ON,Only English)]_Test1("(ShowHeader)A可开启显示",color) = (1,1,1,1)
		//[ShowHeader(_A_ON,_B_ON,Only English)]_Test2("(ShowHeader)A与B可开启显示",color) = (1,1,1,1)
		////----参数中的1没有实际的意义，任何数值都可以，只是作为分隔符使用
		//[ShowKeywordEnum(_A_ON,1, None, Add, Multiply,a,b,c,d,e,f,g)] _KeywordTest1("(ShowKeywordEnum)A可开启显示", Float) = 0
		//[ShowKeywordEnum(_A_ON,_B_ON,1, None, Add, Multiply,a,b,c,d,e,f,g)] _KeywordTest2("(ShowKeywordEnum)A与B可开启显示", Float) = 0
		////----
		//[ShowEnum(_A_ON,UnityEngine.Rendering.BlendMode)] _Blend1("Blend1", Float) = 1
		//[ShowEnum(_A_ON,UnityEngine.Rendering.CullMode)] _Blend2("Blend2", Float) = 1
		//[ShowEnum(_A_ON,UnityEngine.Rendering.CompareFunction)] _Blend3("Blend3", Float) = 1
		//[ShowEnum(_A_ON,_B_ON,UnityEngine.Rendering.BlendMode)] _Blend4("Blend4", Float) = 1
		//[ShowEnum(_A_ON,_B_ON,UnityEngine.Rendering.CullMode)] _Blend5("Blend5", Float) = 1
		//[ShowEnum(_A_ON,_B_ON,UnityEngine.Rendering.CompareFunction)] _Blend6("Blend6", Float) = 1
		//[ShowEnum(_A_ON,One,1,SrcAlpha,5)] _Blend7("Blend7", Float) = 1
		//[ShowEnum(_A_ON,_B_ON,One,1,SrcAlpha,5,AAAA,7)] _Blend8("Blend8", Float) = 1
		[ShowBasedProperty(_SlideToggle,0.5)]_Based1("Base1",Color) = (1,1,1,1)
		

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
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return 1;
			}
			ENDCG
		}
	}
}
