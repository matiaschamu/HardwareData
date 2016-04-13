using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Forms;


namespace BibliotecaMaf.Clases.HardwareData
{
    public static class HardwareData
    {
		public struct Drive
		{
			public readonly string Name;
			public readonly string Letra;
			public readonly string Tamaño;
			public readonly string TamañoOcupado;
			public readonly string TamañoLibre;



			public Drive(string name, string tamaño, string tamañoOcupado, string tamañoLibre, string letra) : this()
			{
				Name = name;
				Tamaño = tamaño;
				TamañoOcupado = tamañoOcupado;
				TamañoLibre = tamañoLibre;
				Letra = letra;
			}
		}

	    private static string _nombreProcesador = "";
	    private static string _idProcesador = "";
	    private static string _bitsProcesador = "";
	    private static string _systemaOperativo = "";
	    private static string _versionOs = "";
	    private static string _numerodeSerie = "";
	    private static string _build = "";
	    private static string _plataforma = "";


	    public static DriveInfo[] Unidades
	    {
		    get
		    {
			    return  System.IO.DriveInfo.GetDrives();	
		    }
	    }

	    public static string NombreProcesador
	    {
		    get { return _nombreProcesador; }
	    }

	    public static string IdProcesador
	    {
		    get { return _idProcesador; }
	    }

	    public static string BitsProcesador
	    {
		    get { return _bitsProcesador; }
	    }

	    public static string SystemaOperativo
	    {
		    get { return _systemaOperativo; }
	    }

	    public static string VersionOs
	    {
		    get { return _versionOs; }
	    }

	    public static string NumerodeSerie
	    {
		    get { return _numerodeSerie; }
	    }

	    public static string Build
	    {
		    get { return _build; }
	    }

	    public static string Plataforma
	    {
		    get { return _plataforma; }
	    }

	    public static new  string ToString()
	    {
		    string mInformacion = "";
			CargarDatos();
			mInformacion = "MicroID: " + IdProcesador + "\r\n";
			mInformacion = mInformacion + "Procesador: " + NombreProcesador + " - " + BitsProcesador + "\r\n";
			mInformacion = mInformacion + "Pantalla: " + Screen.PrimaryScreen.Bounds.Width + " x " + Screen.PrimaryScreen.Bounds.Height + "\r\n";
			mInformacion = mInformacion + "NombrePC: " + System.Environment.MachineName + "\r\n";
			mInformacion = mInformacion + "Windows: " + SystemaOperativo + " - " + Plataforma + "\r\n";
			mInformacion = mInformacion + "Numero de serie OS: " + NumerodeSerie + "\r\n";
			mInformacion = mInformacion + "Version Windows: " + VersionOs + " Build: " + Build + "\r\n";
			return mInformacion;
	    }


		private static void CargarDatos()
		{
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_Processor");
			foreach (ManagementBaseObject mManagementBaseObject in searcher.Get())
			{
				_nombreProcesador = mManagementBaseObject.Properties["Name"].Value.ToString();
				_idProcesador = mManagementBaseObject.Properties["ProcessorId"].Value.ToString();
				_bitsProcesador = mManagementBaseObject.Properties["DataWidth"].Value.ToString() + " bits";
				break;

				foreach (PropertyData mPropertyData in mManagementBaseObject.Properties)
				{
					Console.WriteLine(mPropertyData.Name + " - " + mPropertyData.Value);
				}
			}

			searcher = new ManagementObjectSearcher("select * from " + "Win32_OperatingSystem");
			foreach (ManagementBaseObject mManagementBaseObject in searcher.Get())
			{
				_systemaOperativo = mManagementBaseObject.Properties["Caption"].Value.ToString();
				_versionOs = mManagementBaseObject.Properties["Version"].Value.ToString();
				_numerodeSerie = mManagementBaseObject.Properties["SerialNumber"].Value.ToString();
				_build = mManagementBaseObject.Properties["BuildNumber"].Value.ToString();
				_plataforma = mManagementBaseObject.Properties["OSArchitecture"].Value.ToString();

				foreach (PropertyData mPropertyData in mManagementBaseObject.Properties)
				{
					Console.WriteLine(mPropertyData.Name + " - " + mPropertyData.Value);
				}
			}
		}
    }
}
