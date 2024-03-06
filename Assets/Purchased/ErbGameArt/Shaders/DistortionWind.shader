Shader "ErGameArt/DistortionWind" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0,0.362,1,1)
        _Noise ("Noise", 2D) = "black" {}
        [MaterialToggle] _Usedistortion ("Use distortion?", Float ) = 0
        _Emission ("Emission", Float ) = 3
        _UVDistortionOpacity ("U / V / Distortion / Opacity", Vector) = (-1,0,1,0.4)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform fixed _Usedistortion;
            uniform float _Emission;
            uniform float4 _UVDistortionOpacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float2 node_1505 = ((float2(_UVDistortionOpacity.r,_UVDistortionOpacity.g)*_Time.g)+i.uv0);
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_1505, _Noise));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = (lerp( 0.0, saturate(tex2D( _GrabTexture, (((_Noise_var.r*_Noise_var.r)*_UVDistortionOpacity.b)+i.uv0)).rgb), _Usedistortion )+(_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*_Emission*_Noise_var.r));
                fixed4 finalRGBA = fixed4(emissive,(_MainTex_var.a*i.vertexColor.a*_TintColor.a*_UVDistortionOpacity.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
}
