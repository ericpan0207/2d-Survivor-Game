Shader "Custom/Shield" {
     Properties {
         _Position ("Collision", Vector) = (-1, -1, -1, -1)        
         _MaxDistance ("Effect Size", float) = 3       
         _ShieldColor ("Color (RGBA)", Color) = (0.7, 1, 1, 0.2)
         _EmissionColor ("Emission color (RGBA)", Color) = (0.7, 1, 1, 0.01)        
		 _EffectTime ("Effect Time (ms)", float) = 0
     }
     
     SubShader {
         Tags { "Queue" = "Transparent" "RenderType" = "Opaque" }
         LOD 2000
         Cull Off
       
         CGPROGRAM
           #pragma surface surf Lambert vertex:vert alpha
           #pragma target 3.0
       
          struct Input {
              float customDist;
           };
       
           float4 _Position;          
           float _MaxDistance;          
           float4 _ShieldColor;
           float4 _EmissionColor;          
           float _EffectTime;
           
           float _Amount;
       
           void vert (inout appdata_full v, out Input o) {
               o.customDist = distance(_Position.xyz, v.vertex.xyz);			   
           }
       
           void surf (Input IN, inout SurfaceOutput o) {
               o.Albedo = _ShieldColor.rgb;
               o.Emission = _EmissionColor;
             
               if(_EffectTime > 0)
               {
                    if(IN.customDist < _MaxDistance){
                        o.Alpha = _EffectTime/350 -(IN.customDist / _MaxDistance) + _ShieldColor.a;
						//o.Alpha = _EffectTime/350 - (IN.customDist / _MaxDistance) + _ShieldColor.a;
                        if(o.Alpha < _ShieldColor.a){
                            o.Alpha = _ShieldColor.a;
                        }
                    }
                    else {
                        o.Alpha = _ShieldColor.a;
                    }
               }
			   else if(_EffectTime <= -1000) 
			   {			//Fade           Location of fade/speed?       magnitude of fade
					o.Alpha = 400/-_EffectTime + (IN.customDist /20) - _ShieldColor.a * 1.2;					
			   }
               else{
                   o.Alpha = _ShieldColor.a;
               }
           }
       
           ENDCG
     } 
     Fallback "Transparent/Diffuse"
 }