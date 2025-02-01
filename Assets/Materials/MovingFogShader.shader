Shader "Custom/MovingFogShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _FogColor ("Fog Color", Color) = (0.7, 0.7, 0.7, 1)
        _FogDensity ("Fog Density", Range(0, 3)) = 1.0
        _Speed ("Fog Speed", Range(0, 3)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _FogColor;
            float _FogDensity;
            float _Speed;

            // Simple noise function for fog randomness
            float noise(float2 p)
            {
                return frac(sin(dot(p.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the sprite texture
                fixed4 color = tex2D(_MainTex, i.uv);

                // Generate fog noise and animation
                float timeOffset = _Time * _Speed;
                float fogNoise = noise(i.uv * 5.0 + timeOffset);

                // Apply fog intensity and density
                float fogFactor = saturate(1.0 - _FogDensity * fogNoise);

                // Blend fog with sprite color
                return lerp(_FogColor, color, fogFactor);
            }
            ENDCG
        }
    }
}