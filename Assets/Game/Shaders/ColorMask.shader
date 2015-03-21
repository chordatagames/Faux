Shader "Custom/ColorMask" {
	Properties 
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_BumpTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Mask Color", Color) = (1,1,1,1)
	}
	SubShader
	{	
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf Lambert alpha	
		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_BumpTex;
		}; 
      	sampler2D _MainTex;
       	sampler2D _BumpTex;
        half4 _Color; //replacement color
       
       	void surf (Input IN, inout SurfaceOutput o) 
       	{
        	half4 c = tex2D(_MainTex, IN.uv_MainTex) + tex2D(_BumpTex, IN.uv_BumpTex).a * _Color;
        	o.Emission = c.a * c.rgb;
        	o.Alpha = c.a;
        }
        ENDCG
 	}
	Fallback "Diffuse"
}