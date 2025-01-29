Shader "Unlit/PulseShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}  // The sprite's texture
        _HitEffect ("Hit Effect", Range(0,1)) = 0     // Pulse intensity (0 = normal, 1 = fully colored)
        _HitColor ("Hit Color", Color) = (1, 0, 0, 1) // Customizable hit color
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha  // Enables transparency

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
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float _HitEffect;
            fixed4 _HitColor; // Custom hit color

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 spriteColor = tex2D(_MainTex, i.uv); // Sample the sprite texture
                fixed4 hitColor = fixed4(_HitColor.rgb, spriteColor.a); // Custom color with sprite transparency
                
                // Blend between sprite color and hit color
                return lerp(spriteColor, hitColor, _HitEffect);
            }
            ENDCG
        }
    }
}

