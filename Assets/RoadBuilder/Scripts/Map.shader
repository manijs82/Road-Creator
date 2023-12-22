Shader "Unlit/Map"
{
    Properties
    {
        _RoadTexture ("RoadTexture", 2D) = "white" {}
        _RoadColor ("RoadColor", Color) = (0, 0, 1, 1)
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _RoadTexture;
            float4 _RoadTexture_ST;
            float4 _RoadColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _RoadTexture);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                const fixed4 white = fixed4(1, 1, 1, 1);
                fixed4 col = tex2D(_RoadTexture, i.uv);

                if (any(col.rgb != white.rgb))
                    return _RoadColor;
                return col;
            }
            ENDCG
        }
    }
}