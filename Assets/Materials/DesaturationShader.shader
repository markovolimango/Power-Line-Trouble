Shader "Custom/DesaturationShader"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _DesaturationAmount("Desaturation (0: Full Color, 1: Grayscale)", Range(0, 1)) = 0.5
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
            float _DesaturationAmount;

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
                float grayscale = dot(color.rgb, fixed3(0.299, 0.587, 0.114));
                color.rgb = lerp(color.rgb, grayscale.xxx, _DesaturationAmount);
                return color;
            }
            ENDCG
        }
    }
}