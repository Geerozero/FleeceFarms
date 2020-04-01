Shader "Hidden/SimpleShader"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_bwBlend("Black & White blend", Range(0, 1)) = 0
		_red("Red Intensity", Range(0, 1)) = 0
		_green("Green Intensity", Range(0, 1)) = 0
		_blue("Blue Intensity", Range(0, 1)) = 0
		_r_power("Red Intensity", Range(0, 3)) = 0
		_g_power("Red Intensity", Range(0, 3)) = 0
		_b_power("Red Intensity", Range(0, 3)) = 0
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _bwBlend;
			uniform float _red;
			uniform float _blue;
			uniform float _green;
			uniform int _r_power;
			uniform int _g_power;
			uniform int _b_power;
			uniform float _r_y_offset;
			uniform float _b_y_offset;
			

			float4 frag(v2f_img i) : COLOR
			{
				float4 c = tex2D(_MainTex, i.uv);

				float lum = c.r*.3 + c.g*.59 + c.b*.11;
				float3 bw = float3(lum, lum, lum);

				float4 result = c;
				float offset = .5;
				float temp = 0.0f;
				
				// red color component
				if (_r_power == 3)
				{
					result.r = lerp(c.r, (4 * pow(c.r - offset, 3.0)) + .5, _bwBlend * _red);
				}
				else if (_r_power == 2)
				{
					result.r = lerp(c.r, (2 * pow(c.r - offset, 2.0)) + .5, _bwBlend * _red);
				}
				else if (_r_power == 0)
				{
					temp = lerp(result.r, _red,  _red * _bwBlend);
					
					if (temp > .5)
					{
						if (temp > .8)
						{
							temp = .8;
						}
						else
						{
							temp = .8;
						}
					}
					else
					{
						if (temp < .2)
						{
							temp = .1;
						}
						else
						{
							temp = .3;
						}
					}

					result.r = temp;
				}

				// blue color component
				if (_b_power == 3)
				{
					result.b = lerp(c.b, (4 * pow(c.b - offset, 3.0)) + .5, _bwBlend * _blue);
				}
				else if (_b_power == 2)
				{
					result.b = lerp(c.b, (2 * pow(c.b - offset, 2.0)) + .5, _bwBlend * _blue);
				}
				else if(_b_power == 0)
				{
					temp = lerp(result.b, _blue, _blue * _bwBlend);

					if (temp > .5)
					{
						if (temp > .8)
						{
							temp = .8;
						}
						else
						{
							temp = .8;
						}
					}
					else
					{
						if (temp < .2)
						{
							temp = .1;
						}
						else
						{
							temp = .3;
						}
					}

					result.b = temp;
				}

				// green color component
				if (_g_power == 3)
				{
					result.g = lerp(c.g, (4 * pow(c.g - offset, 3.0)) + .5, _bwBlend * _green);
				}
				else if (_g_power == 2)
				{
					result.g = lerp(c.g, (2 * pow(c.g - offset, 2.0)) + .5, _bwBlend * _green);
				}
				else if (_g_power == 0)
				{
					temp = lerp(result.g, _green, _green * _bwBlend);

					if (temp > .5)
					{
						if (temp > .8)
						{
							temp = .8;
						}
						else
						{
							temp = .8;
						}
					}
					else
					{
						if (temp < .2)
						{
							temp = .1;
						}
						else
						{
							temp = .3;
						}
					}
					result.g = temp;
				}
	
				return result;
			}
			ENDCG
		}
	}
}