// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "GradientShader"
{
	Properties
	{
		_Albeo("Albeo", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.5
		_Metallic("Metallic", Range( 0 , 1)) = 0
		[Header(Gradient Settings)][Space(10)]_TopColor("TopColor", Color) = (1,1,1,1)
		_BottomColor("BottomColor", Color) = (0,0,0,0)
		[Toggle]_GradientAlongX("Gradient Along X", Float) = 0
		[Toggle]_GradientAlongZ("Gradient Along Z", Float) = 0
		_From("From", Float) = 0
		_To("To", Float) = 1
		_Spread("Spread", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _Albeo;
		uniform float4 _Albeo_ST;
		uniform float4 _TopColor;
		uniform float4 _BottomColor;
		uniform float _Spread;
		uniform float _GradientAlongZ;
		uniform float _GradientAlongX;
		uniform float _To;
		uniform float _From;
		uniform float _Metallic;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Albeo = i.uv_texcoord * _Albeo_ST.xy + _Albeo_ST.zw;
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float smoothstepResult26 = smoothstep( _Spread , 1.0 , saturate( ( (( _GradientAlongZ )?( ( _To - ase_vertex3Pos.z ) ):( (( _GradientAlongX )?( ( _To - ase_vertex3Pos.x ) ):( ( _To - ase_vertex3Pos.y ) )) )) / ( _To - _From ) ) ));
			float4 lerpResult8 = lerp( _TopColor , _BottomColor , smoothstepResult26);
			float4 GradientColorCalcualtion18 = lerpResult8;
			o.Albedo = ( tex2D( _Albeo, uv_Albeo ) * GradientColorCalcualtion18 ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
-1913;24;1920;995;2577.49;893.0286;2.330746;True;False
Node;AmplifyShaderEditor.CommentaryNode;20;-4107.353,-169.0412;Inherit;False;2132.745;892.0775;Comment;17;27;18;8;2;26;1;12;11;15;9;3;17;14;10;16;4;5;GradientCalcualtion;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;5;-4097.127,324.1912;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-4078.045,133.8607;Inherit;False;Property;_To;To;8;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;16;-3602.066,113.0491;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;-3612.758,237.511;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;14;-3368.066,229.249;Inherit;False;Property;_GradientAlongX;Gradient Along X;5;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;17;-3615.067,345.5492;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-4061.27,523.9303;Inherit;False;Property;_From;From;7;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;15;-3121.066,313.2491;Inherit;False;Property;_GradientAlongZ;Gradient Along Z;6;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;9;-3626.978,503.4101;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;11;-2897.813,477.5385;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;12;-2752.372,475.5395;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-2913.677,594.5646;Inherit;False;Property;_Spread;Spread;9;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-2795.991,67.26991;Inherit;False;Property;_BottomColor;BottomColor;4;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;26;-2553.84,532.9972;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-2795.141,-119.0412;Inherit;False;Property;_TopColor;TopColor;3;1;[Header];Create;True;1;Gradient Settings;0;0;False;1;Space(10);False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;8;-2455.378,50.29418;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;18;-2236.721,43.92968;Inherit;False;GradientColorCalcualtion;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;21;-903.4131,-151.0611;Inherit;True;Property;_Albeo;Albeo;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;22;-789.2347,57.60965;Inherit;False;18;GradientColorCalcualtion;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-513.3172,368.5018;Inherit;False;Property;_Metallic;Metallic;2;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;32;-742.6903,579.1954;Inherit;False;18;GradientColorCalcualtion;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-324.6479,-68.3801;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;33;-379.8819,583.983;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;25;-509.4168,458.202;Inherit;False;Property;_Smoothness;Smoothness;1;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;15.60001,38.99999;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;GradientShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;4;0
WireConnection;16;1;5;1
WireConnection;10;0;4;0
WireConnection;10;1;5;2
WireConnection;14;0;10;0
WireConnection;14;1;16;0
WireConnection;17;0;4;0
WireConnection;17;1;5;3
WireConnection;15;0;14;0
WireConnection;15;1;17;0
WireConnection;9;0;4;0
WireConnection;9;1;3;0
WireConnection;11;0;15;0
WireConnection;11;1;9;0
WireConnection;12;0;11;0
WireConnection;26;0;12;0
WireConnection;26;1;27;0
WireConnection;8;0;1;0
WireConnection;8;1;2;0
WireConnection;8;2;26;0
WireConnection;18;0;8;0
WireConnection;23;0;21;0
WireConnection;23;1;22;0
WireConnection;33;0;32;0
WireConnection;0;0;23;0
WireConnection;0;3;24;0
WireConnection;0;4;25;0
WireConnection;0;9;33;3
ASEEND*/
//CHKSM=3C60B11E144EAE3B7C825CFD52A50D6DEB920BED