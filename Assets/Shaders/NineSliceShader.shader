Shader "Custom/NineSliceShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DisplayRect ("Display Rect", Vector) = (0, 0, 1, 1) // (xMin, yMin, xMax, yMax)
        _SpriteSize ("Sprite Size", Vector) = (1, 1, 1, 1)  // (Width, Height) of the sprite
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _DisplayRect; // 表示する範囲を指定 (xMin, yMin, xMax, yMax)
            float4 _SpriteSize;  // Sprite size in world space

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;

                // 横幅や縦幅に基づいて表示範囲を補正
                float aspectRatio = _SpriteSize.x / _SpriteSize.y;
                o.uv.x = o.uv.x * aspectRatio;  // 横幅に基づいて調整

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 表示範囲が指定されたRect内に入っているかチェック
                if (i.uv.x < _DisplayRect.x || i.uv.x > _DisplayRect.z ||
                    i.uv.y < _DisplayRect.y || i.uv.y > _DisplayRect.w)
                    discard;

                // テクスチャカラーとSpriteRendererのColorを掛け合わせる
                fixed4 texColor = tex2D(_MainTex, i.uv) * i.color;

                // 透過部分を維持
                if (texColor.a < 0.01)
                    discard;

                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Transparent/VertexLit"
}