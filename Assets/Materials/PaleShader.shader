Shader "Custom/PaleEffectShader"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _PaleAmount("Pale Amount (0: None, 1: Full)", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _PaleAmount;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
                
                // Compute pale color by blending toward white
                float3 paleColor = lerp(color.rgb, float3(1.0, 1.0, 1.0), _PaleAmount);

                return fixed4(paleColor, color.a);
            }
            ENDCG
        }
    }
}