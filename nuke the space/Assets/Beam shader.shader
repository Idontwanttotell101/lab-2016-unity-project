Shader "Custom/Beam shader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_ColorWeight("ColorWeight", Range(0,1)) = 0.5
		_PScale("world coordinate weight", Vector) = (1,1,1)
		_TScale("time weight", Vector) = (-50,5,5)
		_Period("period", Vector) = (1,2,3)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
			Tags { "RenderType" = "Transparent" }
			LOD 200

			CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float3 _PScale;
		float3 _TScale;
		float3 _Period;
		float _ColorWeight;

		void surf(Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = _Color;

			fixed3 sampleColor;
			sampleColor.r = (sin((IN.worldPos.x*_PScale.x + _Time.y * _TScale.x) / _Period.x) + 1) / 2;
			sampleColor.g = (sin((IN.worldPos.y*_PScale.y + _Time.y * _TScale.y) / _Period.y) + 1) / 2;
			sampleColor.b = (sin((IN.worldPos.z*_PScale.z + _Time.y * _TScale.z) / _Period.z) + 1) / 2;
			c.rgb = sampleColor*(1- _ColorWeight) + c.rgb*_ColorWeight;
			o.Albedo = c.rgb;

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
		FallBack "Diffuse"
}
