Shader "Custom/ProjectionMesh"
{
    Properties
    {
        _InactiveColour ("Inactive Colour", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _InactiveColour;
            int displayMask; // set to 1 to display texture, otherwise will draw test colour
            float4x4 _ProjectionMatrix;
            float4x4 _ViewMatrix;
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.vertex = v.vertex;
                // o.screenPos = mul(modelviewproj, o.vertex).xyw;
                o.screenPos = mul(_ProjectionMatrix, mul(_ViewMatrix, mul(unity_ObjectToWorld, v.vertex)));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.screenPos.xy / i.screenPos.w;
                uv = uv * 0.5 + 0.5; 
                // fixed4 portalCol = tex2D(_MainTex, uv);
                return tex2D(_MainTex, uv);
                // return portalCol * displayMask + _InactiveColour * (1-displayMask);
            }
            ENDCG
        }
    }
    Fallback "Standard" // for shadows
}
