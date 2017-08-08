// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ImageEffects/WaterImageEffect"
{
	Properties
	{
		[HideInInspector]
		_MainTex("Texture", 2D) = "white" {}		
		_OverlayTex("Overlay Texture", 2D) = "white" {}
		_OverlayBlend("Overlay Blend", Range(0,1)) = 0.5
		_WarpTex("Warp Texture", 2D) = "white" {}
		_WarpStrength("Warp Strength", Range(0, .1)) = 0
		_Direction("Direction", Vector) = (0,0,0,0)
		_Speed("Speed", Float) = 0
		_RotationSpeed("Warp Speed", Float) = 2.0

	}
	SubShader
	{
		Tags{"RenderType" = "Transparent"}
		// No culling or depth
		Blend One SrcAlpha
		Cull Off ZWrite Off ZTest Always

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
				float2 texcoord : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			
			sampler2D _MainTex;
			sampler2D _OverlayTex;
			float4 _OverlayTex_ST;
			sampler2D _WarpTex;
			float _OverlayBlend;
			float _WarpStrength;
			float2 _Direction;			
			float _Speed;
			float _RotationSpeed;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				float sinX = sin(_RotationSpeed * _Time);
				float cosX = cos(_RotationSpeed * _Time);
				float2x2 rotationMatrix = float2x2 (cosX, -sinX, sinX, cosX);
				o.texcoord = mul(v.uv, rotationMatrix);
				return o;
			}

			float4 frag (v2f i) : SV_Target
			{								
				float2 disp = tex2D(_WarpTex, i.texcoord).xy;
				disp = ((disp * 2) - 1) * _WarpStrength;

				float4 col = tex2D(_MainTex, i.uv + disp);
				float4 col2 = tex2D(_OverlayTex, (i.uv + disp) * _OverlayTex_ST.xy + _Time.x * _Speed * normalize(_Direction.xy)) * float4(1,1,1, _OverlayBlend);

				col *= col2;

				return col;
			}
			ENDCG
		}
	}	
}
