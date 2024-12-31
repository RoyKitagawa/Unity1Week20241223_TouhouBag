Shader "Custom/GlowEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (1, 1, 0, 1)
        _GlowIntensity ("Glow Intensity", Range(0, 10)) = 10.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _GlowColor;
            float _GlowIntensity;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.texcoord);
                fixed4 glow = _GlowColor * _GlowIntensity;
                return texColor + glow * texColor.a; // 発光効果を加算
            }
            ENDCG
        }
    }
}