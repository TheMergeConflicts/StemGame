Shader "StemGame/Distort" {
Properties {
	_Refraction ("Refraction", Range (0.00, 100.0)) = 1.0
	_MainTex ("Base (RGB)", 2D) = "b" {}
}

SubShader 
{	
	Tags { "RenderType"="Transparent" "Queue"="Overlay" }
	LOD 100
	
	GrabPass 
	{ 
		
	}
	
CGPROGRAM
#pragma exclude_renderers gles

#pragma surface surf NoLighting
#pragma vertex vert



fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
    {
        fixed4 c;
        c.rgb = s.Albedo; 
        c.a = s.Alpha;
        return c;
    }

sampler2D _GrabTexture;// : register(s0);
sampler2D _MainTex;// : register(s2);
float _Refraction;

float4 _GrabTexture_TexelSize;

struct Input {
	float2 uv_MainTex;
	float3 color;
	float3 worldRefl; 
	float4 screenPos;
	INTERNAL_DATA
};

void vert (inout appdata_full v, out Input o) {
  UNITY_INITIALIZE_OUTPUT(Input,o);
  o.color = v.color;
}

void surf (Input IN, inout SurfaceOutput o) 
{
    float3 distort = tex2D(_MainTex, IN.uv_MainTex);// * IN.color.rgb;
    float2 offset = distort *_Refraction;//*(_GrabTexture_TexelSize.xy);
	IN.screenPos.y = 1-IN.screenPos.y;
	float4 normalStuff = tex2Dproj(_GrabTexture, IN.screenPos);
	IN.screenPos.xy = (offset * IN.screenPos.z)+ IN.screenPos.xy;	
	//offset = (IN.uv_MainTex.xy-float2(.5,0))/abs((IN.uv_MainTex.xy-float2(.5,.5)));
	//offset = (float3(1,1,1)-distort.rgb)*offset * _Refraction;
	//IN.screenPos.x = offset * IN.screenPos.z + IN.screenPos.x;
	//IN.screenPos.y = -offset * IN.screenPos.z + IN.screenPos.y;



	float4 refrColor = tex2Dproj(_GrabTexture, IN.screenPos);
	o.Alpha = refrColor.a;
	o.Emission = refrColor.rgb;//*1.4;
	//o.Emission = refrColor.rgb+tex2D(_MainTex, IN.uv_MainTex)*refrColor.rgb;
	//o.Emission = float4(1,0,0,1);
}
ENDCG
}
FallBack "Diffuse"
}