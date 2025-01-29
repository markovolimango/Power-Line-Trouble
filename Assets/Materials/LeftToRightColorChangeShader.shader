Shader "Unlit/LeftToRightColorChangeShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {} // Support for SpriteRenderer
        _StartColor ("Start Color", Color) = (1, 1, 0, 1) // Default yellow
        _TransitionProgress ("Transition Progress", Range(0, 1)) = 0 // Controls transition progress
        _Smoothness ("Edge Smoothness", Range(0, 1)) = 0.1 // Controls blending width
        _TransitionMode ("Transition Mode (0 = To Yellow, 1 = To Original)", Range(0, 1)) = 0 // 0 = to yellow, 1 = to original
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha // Preserve transparency
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
            fixed4 _StartColor;
            float _TransitionProgress;
            float _Smoothness;
            float _TransitionMode; // 0 = to yellow, 1 = to original color

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 spriteColor = tex2D(_MainTex, i.uv); // Get original texture color
                fixed4 startColor = fixed4(_StartColor.rgb, spriteColor.a); // Start color (yellow)

                // Calculate the transition factor based on the UV coordinates
                float transitionFactor = i.uv.x;

                // Smooth transition using the _TransitionProgress
                float blendFactor = smoothstep(_TransitionProgress - _Smoothness, _TransitionProgress + _Smoothness, transitionFactor);

                // Blend from yellow to original color based on the transition progress
                return lerp(startColor, spriteColor, _TransitionMode == 0 ? blendFactor : 1.0 - blendFactor);
            }
            ENDCG
        }
    }
}
