Shader "Custom/CooldownWithGlow"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Progress ("Cooldown Progress", Range(0, 1)) = 1
        _GlowColor ("Glow Color", Color) = (1, 1, 0, 0.5)
        _GlowThickness ("Glow Thickness", Float) = 0.05
        _UseCooldownEffect ("Use Cooldown Effect (0=Off, 1=On)", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
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
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Progress;
            float4 _GlowColor;
            float _GlowThickness;
            float _UseCooldownEffect;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.texcoord;

                // テクスチャの元の色
                fixed4 col = tex2D(_MainTex, uv);

                // Cooldownエフェクトが無効な場合はそのまま返す
                if (_UseCooldownEffect < 0.5)
                {
                    return col;
                }

                // グラデーションの範囲計算
                float glowStart = _Progress - (_GlowThickness * 0.5);
                float glowEnd = _Progress + (_GlowThickness * 0.5);

                // Progressを中心にグラデーション
                float lowerGlowIntensity = smoothstep(glowStart, _Progress, uv.y);
                float upperGlowIntensity = smoothstep(glowEnd, _Progress, uv.y);

                // Glowのフェードを計算
                float glowIntensity = 0.0;
                if (uv.y < _Progress)
                {
                    glowIntensity = lowerGlowIntensity;
                }
                else if (uv.y >= _Progress)
                {
                    glowIntensity = upperGlowIntensity;
                }

                fixed4 glow = glowIntensity * _GlowColor;

                // 色の調整：Progressを境に色味を変更
                if (uv.y >= _Progress)
                {
                    col.rgb *= 0.3; // 上側は7割暗く
                }

                // Glowをアルファ値を考慮して合成
                if (uv.y > glowStart && uv.y < glowEnd)
                {
                    col.rgb = lerp(col.rgb, col.rgb + glow.rgb, glow.a); // Glowのアルファ値を反映
                }

                return col;
            }
            ENDCG
        }
    }
}