
Shader "Custom/Edge"  
{  
    Properties  
    {  
        _OutSideCircular ("OutSideCircular", Range(0, 1)) = 0.043  
		_InSideCircular("InSideCircular",Range(0,1))=0.02
        _EdgeColor ("EdgeColor", Color) = (1, 1, 1, 1)  
		_FlowColor ("FlowColor", Color) = (1, 1, 1, 1) 
		_FlowSpeed ("FlowSpeed", Range(0, 10)) = 3
		_MainTex ("MainTex", 2D) = "white" {}  
		_MainTexAlpha("MainTexAlpha",Range(0,1))=0.9
    }  


    SubShader  
    {  
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" } 

        Pass  
        {  
			ZWrite Off  

			Blend SrcAlpha OneMinusSrcAlpha 


            CGPROGRAM  
            #pragma vertex vert  
            #pragma fragment frag  

            #include "UnityCG.cginc"  

            fixed _OutSideCircular;  
			fixed _InSideCircular;

            fixed4 _EdgeColor;  

			fixed4 _FlowColor;

			float _FlowSpeed;

			sampler2D _MainTex;

			float _MainTexAlpha;

            struct appdata  
            {  
                float4 vertex : POSITION;  
                fixed2 uv : TEXCOORD0;  
            };  


            struct v2f  
            {  
                float4 vertex : SV_POSITION;  

                fixed2 uv : TEXCOORD1;  
            };  


            v2f vert (appdata v)  
            {  
                v2f o;  

                o.vertex = UnityObjectToClipPos(v.vertex);   
                o.uv = v.uv; 

                return o;  
            }  

    
            fixed4 frag (v2f i) : SV_Target  
            {     
                fixed x = i.uv.x;  

                fixed y = i.uv.y;  
			    
                if((x-0.5)*(x-0.5)+(y-0.5)*(y-0.5)<_OutSideCircular*_OutSideCircular&&(x-0.5)*(x-0.5)+(y-0.5)*(y-0.5)>_InSideCircular*_InSideCircular)   
                {  
					//点旋转公式：
					//假设对图片上任意点(x,y)，绕一个坐标点(rx0,ry0)逆时针旋转a角度后的新的坐标设为(x0,y0)，有公式：
					//x0 = (x - rx0) * cos(a) - (y - ry0) * sin(a) + rx0 ;
					//y0 = (x - rx0) * sin(a) + (y - ry0) * cos(a) + ry0 ;
					float a = _Time.y * _FlowSpeed; 
					float2 rotUV;
					x -= 0.5;
					y -= 0.5;
					rotUV.x = x * cos(a) - y * sin(a) + 0.5;
				    rotUV.y = x * sin(a) + y * cos(a) + 0.5;
					
					fixed temp = saturate(rotUV.x - 0.5);//-0.5作用是调整流动颜色的比例

                    return _EdgeColor * (1 - temp) + _FlowColor * temp;
                }  
                else   
                {  
                    fixed4 color = tex2D(_MainTex, i.uv);  
					color.a=_MainTexAlpha;
                    return color; 
                }   
            }  
            ENDCG  
        }  
    }  
}  