﻿Shader "Custom/Fog"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Texture", 2D) = "white" {}
        _FogColor("Fog Color", Color) = (0.3, 0.4, 0.7, 1.0)
        _FogStart("Fog Start", float) = 0
        _FogEnd("Fog End", float) = 0
    }

SubShader
{
    Tags{ "RenderType" = "Transparent" }

    CGPROGRAM

    #include "AutoLight.cginc"
    #pragma surface surf Lambert finalcolor:mycolor vertex:myvert

    struct Input
    {
        float2 uv_MainTex;
        half fog;
    };

    fixed4 _Color;
    fixed4 _FogColor;
    half _FogStart;
    half _FogEnd;
    sampler2D _MainTex;

    void myvert(inout appdata_full v, out Input data)
    {
        UNITY_INITIALIZE_OUTPUT(Input,data);
        float4 pos = mul(unity_ObjectToWorld, v.vertex).xyzw;
        data.fog = saturate((_FogStart - pos.y) / (_FogStart - _FogEnd));
    }

    void mycolor(Input IN, SurfaceOutput o, inout fixed4 color)
    {
        fixed4 fogColor = _FogColor.rgba;
        fixed4 tintColor = _Color.rgba;
        #ifdef UNITY_PASS_FORWARDADD
        fogColor = 0;
        #endif
        color.rgba = lerp(color.rgba * tintColor, fogColor, IN.fog);
    }

    void surf(Input IN, inout SurfaceOutput o)
    {
        // o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgba;
        o.Emission = tex2D(_MainTex, IN.uv_MainTex).rgba;
        // o.Opacity = 
    }

    ENDCG
}

    Fallback "Diffuse"
}