Shader "Custom/ColorMask" {
	Properties {
		_MainTex ("Base (RGBA)", 2D) = "white" {}
		_BumpTex ("Bumpmap (RGBA)", 2D) = "white" {}
		_Color ("Replace Color", Color) = (1,1,1,0) //By default transparent in the alpha channel
	}
	SubShader {	
		Cull Off
		
		CGPROGRAM
		#pragma surface surf Lambert alpha
        	
		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpTex;
		}; 
        sampler2D _MainTex;
        sampler2D _BumpTex;
        half4 _Color; //replacement color
        
        void surf (Input IN, inout SurfaceOutput o) {
        	half4 c = tex2D(_MainTex, IN.uv_MainTex) + tex2D(_BumpTex, IN.uv_BumpTex).a * _Color;
        	o.Emission = c.rgb;
        	o.Alpha = c.a;
        }
        ENDCG
 	}
	Fallback "Diffuse"
}