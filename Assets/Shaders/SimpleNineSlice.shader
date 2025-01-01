Shader "Custom/SimpleNineSlice"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // スプライトのテクスチャ
        _BorderWidth ("Border Width (Left, Right, Bottom, Top)", Vector) = (0.1, 0.1, 0.1, 0.1) // 境界サイズ
        _Color ("Color", Color) = (1, 1, 1, 1) // ベースカラー
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _BorderWidth; // 左右上下の境界線
            float4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // UV座標を取得
                float2 uv = i.uv;

                // 境界線の幅を考慮した処理
                float2 borderMin = _BorderWidth.xy;
                float2 borderMax = 1.0 - _BorderWidth.zw;

                float2 edgeMask = step(borderMin, uv) * step(uv, borderMax);

                // テクスチャの色を取得
                fixed4 col = tex2D(_MainTex, uv);

                // 境界線の内外で色を調整
                col *= edgeMask.x * edgeMask.y;

                return col * _Color; // 色調整とアルファブレンディング
            }
            ENDCG
        }
    }
}