using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Management;
using System.Windows.Forms;


namespace BibliotecaMaf.Clases.HardwareData
{
	public static class HardwareData
	{
		public struct Drive
		{
			public readonly string BytesPerSector;
			public readonly string Description;
			public readonly string DeviceID;
			public readonly string FirmwareRevision;
			public readonly string InterfaceType;
			public readonly string Manufacturer;
			public readonly string MaxBlockSize;
			public readonly string MaxMediaSize;
			public readonly string MediaType;
			public readonly string MinBlockSize;
			public readonly string Model;
			public readonly string Name;
			public readonly string Partitions;
			public readonly string SectorsPerTrack;
			public readonly string SerialNumber;
			public readonly string Size;
			public readonly string Status;
			public readonly string TotalCylinders;
			public readonly string TotalHeads;
			public readonly string TotalSectors;
			public readonly string TotalTracks;
			public readonly string TracksPerCylinder;

			public Drive(string bytesPerSector, string description, string deviceId, string firmwareRevision, string interfaceType, string manufacturer, string maxBlockSize, string maxMediaSize, string mediaType, string minBlockSize, string model, string name, string partitions, string sectorsPerTrack, string serialNumber, string size, string status, string totalCylinders, string totalHeads, string totalSectors, string totalTracks, string tracksPerCylinder)
			{
				BytesPerSector = bytesPerSector;
				Description = description;
				DeviceID = deviceId;
				FirmwareRevision = firmwareRevision;
				InterfaceType = interfaceType;
				Manufacturer = manufacturer;
				MaxBlockSize = maxBlockSize;
				MaxMediaSize = maxMediaSize;
				MediaType = mediaType;
				MinBlockSize = minBlockSize;
				Model = model;
				Name = name;
				Partitions = partitions;
				SectorsPerTrack = sectorsPerTrack;
				SerialNumber = serialNumber;
				Size = size;
				Status = status;
				TotalCylinders = totalCylinders;
				TotalHeads = totalHeads;
				TotalSectors = totalSectors;
				TotalTracks = totalTracks;
				TracksPerCylinder = tracksPerCylinder;
			}
		}

		public struct Processor
		{
			public readonly ushort AddressWidth;
			public readonly uint CurrentClockSpeed;
			public readonly ushort DataWidth;
			public readonly string Description;
			public readonly ushort LoadPercentage;
			public readonly string Manufacturer;
			public readonly uint MaxClockSpeed;
			public readonly string Name;
			public readonly uint NumberOfCores;
			public readonly uint NumberOfLogicalProcessors;
			public readonly string ProcessorId;
			public readonly string Status;

			public Processor(ushort addressWidth, uint currentClockSpeed, ushort dataWidth, string description, ushort loadPercentage, string manufacturer, uint maxClockSpeed, string name, uint numberOfCores, uint numberOfLogicalProcessors, string processorId, string status)
			{
				AddressWidth = addressWidth;
				CurrentClockSpeed = currentClockSpeed;
				DataWidth = dataWidth;
				Description = description;
				LoadPercentage = loadPercentage;
				MaxClockSpeed = maxClockSpeed;
				Name = name;
				NumberOfCores = numberOfCores;
				NumberOfLogicalProcessors = numberOfLogicalProcessors;
				ProcessorId = processorId;
				Status = status;
				Manufacturer = manufacturer;
			}
		}

		private static string _systemaOperativo = "";
		private static string _versionOs = "";
		private static string _numerodeSerie = "";
		private static string _build = "";
		private static string _plataforma = "";

		private static Drive[] _drives;
		private static Processor[] _processors;


		public static DriveInfo[] Unidades
		{
			get { return System.IO.DriveInfo.GetDrives(); }
		}


		public static Processor[] Procesadores
		{
			get
			{
				CargarDatosProcesador();
				return _processors;
			}
		}

		public static string SystemaOperativo
		{
			get
			{
				CargarDatosSystemaOperativo();
				return _systemaOperativo;
			}
		}

		public static string VersionOs
		{
			get
			{
				CargarDatosSystemaOperativo();
				return _versionOs;
			}
		}

		public static string NumerodeSerie
		{
			get
			{
				CargarDatosSystemaOperativo();
				return _numerodeSerie;
			}
		}

		public static string Build
		{
			get
			{
				CargarDatosSystemaOperativo();
				return _build;
			}
		}

		public static string Plataforma
		{
			get
			{
				CargarDatosSystemaOperativo();
				return _plataforma;
			}
		}

		public new static string ToString()
		{
			string mInformacion = "";
			CargarDatosProcesador();
			CargarDatosSystemaOperativo();

			mInformacion = "MicroID: " + _processors[0].ProcessorId + "\r\n";
			mInformacion = mInformacion + "Procesador: " + _processors[0].Name + " - " + _processors[0].DataWidth + " bits\r\n";
			mInformacion = mInformacion + "Pantalla: " + Screen.PrimaryScreen.Bounds.Width + " x " + Screen.PrimaryScreen.Bounds.Height + "\r\n";
			mInformacion = mInformacion + "NombrePC: " + System.Environment.MachineName + "\r\n";
			mInformacion = mInformacion + "Windows: " + _systemaOperativo + " - " + _plataforma + "\r\n";
			mInformacion = mInformacion + "Numero de serie OS: " + _numerodeSerie + "\r\n";
			mInformacion = mInformacion + "Version Windows: " + _versionOs + " Build: " + _build + "\r\n";
			return mInformacion;
		}

		private static void CargarDatosProcesador()
		{
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_Processor");

			var cant = searcher.Get().Count;
			_processors = new Processor[cant];
			int i = 0;

			foreach (ManagementBaseObject mManagementBaseObject in searcher.Get())
			{
				ushort mAddressWidth = (ushort) mManagementBaseObject.Properties["AddressWidth"].Value;
				uint mCurrentClockSpeed = (uint) mManagementBaseObject.Properties["CurrentClockSpeed"].Value;
				ushort mDataWidth = (ushort) mManagementBaseObject.Properties["DataWidth"].Value;
				string mDescription = mManagementBaseObject.Properties["Description"].Value.ToString();
				ushort mLoadPercentage = (ushort) mManagementBaseObject.Properties["LoadPercentage"].Value;
				string mManufacturer = mManagementBaseObject.Properties["Manufacturer"].Value.ToString();
				uint mMaxClockSpeed = (uint) mManagementBaseObject.Properties["MaxClockSpeed"].Value;
				string mName = mManagementBaseObject.Properties["Name"].Value.ToString();
				uint mNumberOfCores = (uint) mManagementBaseObject.Properties["NumberOfCores"].Value;
				uint mNumberOfLogicalProcessors = (uint) mManagementBaseObject.Properties["NumberOfLogicalProcessors"].Value;
				string mProcessorId = mManagementBaseObject.Properties["ProcessorId"].Value.ToString();
				string mStatus = mManagementBaseObject.Properties["Status"].Value.ToString();
				_processors[i] = new Processor(mAddressWidth, mCurrentClockSpeed, mDataWidth, mDescription, mLoadPercentage, mManufacturer, mMaxClockSpeed, mName, mNumberOfCores, mNumberOfLogicalProcessors, mProcessorId, mStatus);
				i++;
			}
		}

		private static void CargarDatosSystemaOperativo()
		{
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_OperatingSystem");
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

		private static void CargarDatosDrives()
		{
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_DiskDrive");

			var cant = searcher.Get().Count;
			_drives = new Drive[cant];
			int i = 0;

			foreach (ManagementBaseObject mManagementBaseObject in searcher.Get())
			{
				try
				{
					var a = mManagementBaseObject.Properties["BytesPerSector"].Value;
					a = (a == null) ? ("") : (a.ToString());
					var a1 = mManagementBaseObject.Properties["Description"].Value;
					a1 = (a1 == null) ? ("") : (a1.ToString());
					var a2 = mManagementBaseObject.Properties["DeviceID"].Value;
					a2 = (a2 == null) ? ("") : (a2.ToString());
					var a3 = mManagementBaseObject.Properties["FirmwareRevision"].Value;
					a3 = (a3 == null) ? ("") : (a3.ToString());
					var a4 = mManagementBaseObject.Properties["InterfaceType"].Value;
					a4 = (a4 == null) ? ("") : (a4.ToString());
					var a5 = mManagementBaseObject.Properties["Manufacturer"].Value;
					a5 = (a5 == null) ? ("") : (a5.ToString());
					var a6 = mManagementBaseObject.Properties["MaxBlockSize"].Value;
					a6 = (a6 == null) ? ("") : (a6.ToString());
					var a7 = mManagementBaseObject.Properties["MaxMediaSize"].Value;
					a7 = (a7 == null) ? ("") : (a7.ToString());
					var a8 = mManagementBaseObject.Properties["MediaType"].Value;
					a8 = (a8 == null) ? ("") : (a8.ToString());
					var a9 = mManagementBaseObject.Properties["MinBlockSize"].Value;
					a9 = (a9 == null) ? ("") : (a9.ToString());
					var a10 = mManagementBaseObject.Properties["Model"].Value;
					a10 = (a10 == null) ? ("") : (a10.ToString());
					var a11 = mManagementBaseObject.Properties["Name"].Value;
					a11 = (a11 == null) ? ("") : (a11.ToString());
					var a12 = mManagementBaseObject.Properties["Partitions"].Value;
					a12 = (a12 == null) ? ("") : (a12.ToString());
					var a13 = mManagementBaseObject.Properties["SectorsPerTrack"].Value;
					a13 = (a13 == null) ? ("") : (a13.ToString());
					var a14 = mManagementBaseObject.Properties["SerialNumber"].Value;
					a14 = (a14 == null) ? ("") : (a14.ToString());
					var a15 = mManagementBaseObject.Properties["Size"].Value;
					a15 = (a15 == null) ? ("") : (a15.ToString());
					var a16 = mManagementBaseObject.Properties["Status"].Value;
					a16 = (a16 == null) ? ("") : (a16.ToString());
					var a17 = mManagementBaseObject.Properties["TotalCylinders"].Value;
					a17 = (a17 == null) ? ("") : (a17.ToString());
					var a18 = mManagementBaseObject.Properties["TotalHeads"].Value;
					a18 = (a18 == null) ? ("") : (a18.ToString());
					var a19 = mManagementBaseObject.Properties["TotalSectors"].Value;
					a19 = (a19 == null) ? ("") : (a19.ToString());
					var a20 = mManagementBaseObject.Properties["TotalTracks"].Value;
					a20 = (a20 == null) ? ("") : (a20.ToString());
					var a21 = mManagementBaseObject.Properties["TracksPerCylinder"].Value;
					a21 = (a21 == null) ? ("") : (a21.ToString());

					_drives[i] = new Drive((string) a, (string) a1, (string) a2, (string) a3, (string) a4, (string) a5, (string) a6, (string) a7, (string) a8, (string) a9, (string) a10, (string) a11, (string) a12, (string) a13, (string) a14, (string) a15, (string) a16, (string) a17, (string) a18, (string) a19, (string) a20, (string) a21);
					i++;
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
