using UnityEngine;
using System.Collections;
using NyARUnityUtils;
using jp.nyatla.nyartoolkit.cs.markersystem;
using jp.nyatla.nyartoolkit.cs.core;

namespace NyARUnityUtils
{
	/// <summary>
	/// This class has an internal raster and handles Texture2D image.
	/// The class is useful to input static image to NyARMarkerSystem.
	/// </summary>
	/// <exception cref='NyARException'>
	/// Is thrown when the ny AR exception.
	/// </exception>
	public class NyARUnitySensor :NyARSensor
	{
	    private NyARUnityRaster _raster;
		/// <summary>
		/// This is constructor.
		/// </summary>
		/// <param name='i_width'>
		/// width of internal raster.
		/// </param>
		/// <param name='i_height'>
		/// height of internal raster.
		/// </param>
		public NyARUnitySensor(int i_width,int i_height): base(new NyARIntSize(i_width,i_height))
		{
	        //Create internal raster (Texture2d does not required vertical flipping.)
	        this._raster = new NyARUnityRaster(new Texture2D(i_width,i_height,TextureFormat.RGB24,true));
	        //update by internal raster
	        base.update(this._raster);
		}
        /// <summary>
        /// Update Internal Raster.
        /// </summary>
        /// <param name='i_input'>
        /// new texture image. Must be same size with Sensor size.
        /// </param>
        public void update(Texture2D i_input)
        {
            //update internal raster
            this._raster = new NyARUnityRaster(i_input,true);
            //@bug パフォーマンス劣化ポイント(Raster処理系の再生成が起こる！)
            base.update(this._raster);
        }
		/// <summary>
		/// This function is dissabled.
		/// See "void update(Texture2D i_input)"
		/// </summary>
		/// <param name='i_input'>
		/// undefined.
		/// </param>
		public override void update(INyARRgbRaster i_input)
		{
            throw new NyARRuntimeException();
		}
		public void dGetGsTex(Texture2D tx)
		{
			NyARIntSize sz=this._raster.getSize();
			int[] s=(int[])this._gs_raster.getBuffer();
			Debug.Log(s.Length);
			Color32[] c=new Color32[sz.w*sz.h];
			for(int i=0;i<sz.h;i++){
				for(int i2=0;i2<sz.w;i2++){
					c[i*sz.w+i2].r=c[i*sz.w+i2].g=c[i*sz.w+i2].b=(byte)s[i*sz.w+i2];
					c[i*sz.w+i2].a=0xff;
				}
			}
			tx.SetPixels32(c);
			tx.Apply( false );
		}		
	}

}

