// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 Shader "Custom/VideoAlpha"
    {
    Properties
    {
        // we have removed support for texture tiling/offset,
        // so make them not be displayed in material inspector
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
// Color property for material inspector, default to white
        _Color ("Main Color", Color) = (1,1,1,1)
         _TransparentColor ("Transparent Color", Color) = (0,0,0,1)
_Threshold ("Threshhold", Float) = 0.01

    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            // use "vert" function as the vertex shader
            #pragma vertex vert
            // use "frag" function as the pixel (fragment) shader
            #pragma fragment frag

            // vertex shader inputs
            struct appdata
            {
                float4 vertex : POSITION; // vertex position
                float2 uv : TEXCOORD0; // texture coordinate
            };

            // vertex shader outputs ("vertex to fragment")
            struct v2f
            {
                float2 uv : TEXCOORD0; // texture coordinate
                float4 vertex : SV_POSITION; // clip space position
            };

            // vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                // transform position to clip space
                // (multiply with model*view*projection matrix)
                o.vertex = UnityObjectToClipPos(v.vertex);
                // just pass the texture coordinate
                o.uv = v.uv;
                return o;
            }
            
            // texture we will sample
            sampler2D _MainTex;

// color from the material
            fixed4 _Color;
            fixed4 _TransparentColor;
half _Threshold;

            // pixel shader; returns low precision ("fixed4" type)
            // color ("SV_Target" semantic)
            fixed4 frag (v2f i) : SV_Target
            {
                // sample texture and return it
                fixed4 col = tex2D(_MainTex, i.uv);
//                return col;
half3 transparent_diff = col.xyz - _TransparentColor.xyz;
             half transparent_diff_squared = dot(transparent_diff,transparent_diff);
if(transparent_diff_squared < _Threshold)
                 discard;
fixed4 new_color = col ;//* _Color;
return new_color;
            }
            ENDCG
        }
    }
}