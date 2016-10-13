Shader "Custom/Beam shader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_PScale("world coordinate weight", Vector) = (1,1,1,1)
		_TScale("world coordinate weight", Vector) = (-50,5,5)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
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

			void surf(Input IN, inout SurfaceOutputStandard o) {
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				float3 xxx = sin(float3(1, 2, 3));
				c.r = (sin(IN.worldPos.x + _Time.y*-1000) + 1) / 2;
				c.g = (sin((IN.worldPos.y + _Time.y * 100) / 2) + 1) / 2;
				c.b = (sin((IN.worldPos.z + _Time.y * 100) / 3) + 1) / 2;
				c.rgb = float3(1, 1, 1)*0.4 + c.rgb*0.6;
				//c.r = (sin(_Time.x*20) + 1) / 2;
				//c.g = (sin(_Time.y*20) + 1) / 2;
				//c.b = (sin(_Time.z*20) + 1) / 2;
				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
