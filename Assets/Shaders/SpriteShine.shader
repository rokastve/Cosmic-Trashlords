
Shader "Cosmic/ShinyDefault"
{
	Properties
	{
		[PerRendererData] 
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_ShineWidth("ShineWidth", Range(0,1)) = 0
		_RoomForScroll("Room For Scroll", int) = 2
		_ScrollSpeed("Scroll Speed", float) = 2.0

		// required for UI.Mask
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			// required for UI.Mask
			Stencil
			{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
				ReadMask[_StencilReadMask]
				WriteMask[_StencilWriteMask]
			}
			ColorMask[_ColorMask]

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
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
				fixed4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
			half _ShineLocation;
			half _ShineWidth;
			int _RoomForScroll;
			half _ScrollSpeed;

			fixed4 SampleSpriteTexture(half2 uv)
			{
				fixed4 color = tex2D(_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D(_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				_ShineLocation += (_Time.y * _ScrollSpeed) % _RoomForScroll;

				half lowLevel = _ShineLocation - _ShineWidth;
				half highLevel = _ShineLocation + _ShineWidth;
				half currentDistanceProjection = (uv.x + uv.y) / 2;
				if (currentDistanceProjection > lowLevel && currentDistanceProjection < highLevel) {
					half whitePower = 1 - (abs(currentDistanceProjection - _ShineLocation) / _ShineWidth);
					color.rgb += _Color.a * whitePower * _Color.rgb;
				}

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture(IN.texcoord);
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}