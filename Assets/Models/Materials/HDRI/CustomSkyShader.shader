Shader "Unlit/CustomSkyShader"
{
    Properties
    {
        _Cubemap("Cubemap", Cube) = "" {}
        _Exposure("Exposure", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Cull Off
        Lighting Off
        ZWrite On
        ZTest LEqual

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            samplerCUBE _Cubemap;
            float _Exposure;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float3 worldNormal : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float3 worldNormal = normalize(i.worldNormal);
                half4 color = texCUBE(_Cubemap, worldNormal) * _Exposure;
                return color;
            }
            ENDCG
        }
    }
    FallBack Off
}
