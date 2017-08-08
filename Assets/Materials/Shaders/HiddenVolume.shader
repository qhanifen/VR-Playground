// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Stencil/HiddenVolume"
{
	Properties
	{
		_Color("Uniform Color", Color) = (1,1,1,1)
		[HideInInspector]
		_VolumePosition("VolumePosition", Vector) = (0,0,0,0)
		_VolumeRadius("VolumeRadius", Float) = 0.5
	}
	SubShader
		{
			Tags{ "Queue" = "Geometry" "RenderType" = "Transparent" }
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				Cull Back
				ZWrite Off

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float2 uv : TEXCOORD0;
					float4 vertex : POSITION;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float2 volumeTex : TEXCOORD1;
					float4 vertex : SV_POSITION;
					float4 worldPos : TEXCOORD2;

				};

				float4 _Color;
				float4x4 _VolumeMatrix;
				float4 _VolumePosition;
				float _VolumeRadius;


				bool sphereHit(float3 p)
				{
					return distance(p, _VolumePosition) == _VolumeRadius;
				}

#define STEPS 64
#define STEP_SIZE 0.01

				bool raymarchHit(float3 position, float3 direction)
				{
					for (int i = 0; i < STEPS; i++)
					{
						if (sphereHit(position))
							return true;
						position += direction * STEP_SIZE;
					}
					return false;

				}


				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.volumeTex = mul(_VolumeMatrix, v.vertex);
					o.worldPos = mul(unity_ObjectToWorld, v.vertex);
					o.uv = v.uv;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
					float4 col = _Color;
					float3 deltaVector = i.worldPos.xyz - _VolumePosition.xyz;
					//float distance = length(deltaVector);
					float sqrDistance = dot(deltaVector, deltaVector);
					float sqrRadius = pow(_VolumeRadius, 2);
					
					//if(distance >= _VolumeRadius)
					if (sqrDistance >= sqrRadius + 0.1)
					{
						discard;
					}
					else if (sqrDistance <= sqrRadius + 0.1 && sqrDistance >= sqrRadius - 0.1)
					{
						col = float4(0, 1, 0, 1);
					}

					return col;
				}
					ENDCG
			}			
		}
}
