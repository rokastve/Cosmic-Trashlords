
Shader "Cosmic/DoorMask" 
{
	Properties { }

	SubShader 
	{

		Tags { "Queue"="Geometry-1" }
		ColorMask 0
		ZWrite Off
		Stencil
		{
			Ref 1
			Comp Always
			Pass Replace
		}

		Pass {
		}
	}
}
