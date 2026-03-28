Shader "Custom/SpriteCleanerWithOutline"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        
        [Header(Alpha Cleanup)]
        _AlphaCutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
        _WhiteTolerance ("Toleransi Warna Putih", Range(0, 1)) = 0.95

        [Header(Outline Settings)]
        [Toggle] _UseOutline ("Aktifkan Outline?", Float) = 0
        _OutlineColor ("Warna Outline", Color) = (0,0,0,1)
        _OutlineThickness ("Ketebalan Outline", Range(0, 5)) = 1
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
            };

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_TexelSize; 
            
            float _AlphaCutoff;
            float _WhiteTolerance;
            
            float _UseOutline;
            fixed4 _OutlineColor;
            float _OutlineThickness;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, IN.texcoord);
                
                // 1. Tentukan apakah piksel saat ini adalah bagian "Solid" dari karakter
                float isWhite = (c.r + c.g + c.b) / 3.0;
                bool isSolid = c.a >= _AlphaCutoff && isWhite <= _WhiteTolerance;

                // 2. LOGIKA OUTLINE (Dijalankan pada piksel yang terpotong/transparan)
                if (_UseOutline > 0.5 && !isSolid)
                {
                    float2 step = _MainTex_TexelSize.xy * _OutlineThickness;
                    
                    // Ambil sampel tetangganya
                    fixed4 nUp = tex2D(_MainTex, IN.texcoord + float2(0, step.y));
                    fixed4 nDown = tex2D(_MainTex, IN.texcoord + float2(0, -step.y));
                    fixed4 nLeft = tex2D(_MainTex, IN.texcoord + float2(-step.x, 0));
                    fixed4 nRight = tex2D(_MainTex, IN.texcoord + float2(step.x, 0));

                    // Cek apakah ada tetangga yang "Solid"
                    bool solidUp = nUp.a >= _AlphaCutoff && ((nUp.r + nUp.g + nUp.b)/3.0) <= _WhiteTolerance;
                    bool solidDown = nDown.a >= _AlphaCutoff && ((nDown.r + nDown.g + nDown.b)/3.0) <= _WhiteTolerance;
                    bool solidLeft = nLeft.a >= _AlphaCutoff && ((nLeft.r + nLeft.g + nLeft.b)/3.0) <= _WhiteTolerance;
                    bool solidRight = nRight.a >= _AlphaCutoff && ((nRight.r + nRight.g + nRight.b)/3.0) <= _WhiteTolerance;

                    // Jika piksel ini mau dibuang TAPI bersebelahan dengan karakter, ubah jadi Outline!
                    if (solidUp || solidDown || solidLeft || solidRight)
                    {
                        c = _OutlineColor;
                        c.rgb *= c.a; // Format standar Unity
                        return c;
                    }
                }

                // 3. Jika bukan karakter solid dan juga bukan outline, baru kita buang!
                if (!isSolid) {
                    clip(-1);
                }

                // Format akhir standar Sprite
                c *= IN.color;
                c.rgb *= c.a; 
                return c;
            }
            ENDCG
        }
    }
}