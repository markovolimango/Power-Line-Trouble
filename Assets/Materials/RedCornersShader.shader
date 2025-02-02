Shader "Unlit/RedCornersShader"
{
      Properties
    {
        _PulseSpeed("Pulse Speed", Range(0.1, 5)) = 1.0
        _PulseStrength("Pulse Strength", Range(0.0, 1.0)) = 0.5
        _TimeScale("Time Scale", Range(0.1, 2.0)) = 1.0
        _Color("Pulse Color", Color) = (1, 0, 0, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

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

            uniform float _PulseSpeed;
            uniform float _PulseStrength;
            uniform float _TimeScale;
            uniform float4 _Color;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // Get the screen space coordinates (from -1 to 1)
                float2 uv = i.uv * 2.0 - 1.0;

                // Calculate distance to screen corners (we'll use the diagonal distance)
                float distToCorner = length(uv);

                // Make the pulse effect (sin wave)
                float pulse = sin(_Time * _PulseSpeed + distToCorner * 10.0) * 0.5 + 0.5;
                pulse = pow(pulse, 2.0); // Square the pulse for a more intense effect
                
                // Apply the pulse to the red color
                float4 color = _Color;
                color.rgb *= pulse * _PulseStrength; // Apply pulse to color intensity

                return color;
            }
            ENDCG
        }
    }
}
