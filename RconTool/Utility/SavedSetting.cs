using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RconTool
{

	/// <summary>
	/// Helper class for saving and loading values in ConfigurationManager.AppSettings.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class SavedSetting<T>
	{

		/// <summary>
		/// The current value of this SavedSetting.
		/// </summary>
		[JsonProperty]
		public T Value { 
			get { return value; } 
			set {
				if (this?.value?.Equals(value) ?? true) { this.value = value; }
				else { this.value = value; OnValueChanged?.Invoke(this, EventArgs.Empty); }
			} 
		}
		private T value;

		/// <summary>
		/// Callback invoked when the SavedSetting's value is assigned a new value that is not equal to the previous value.
		/// </summary>
		public EventHandler OnValueChanged;

		/// <summary>
		/// The key that will be associated with the saved value in the AppSettings collection.
		/// <br>The SaveKey cannot be null, empty, or composed of only whitespace characters.</br>
		/// <br>The SaveKey must be unique, or an existing setting could be overwritten (or overwrite this setting).</br>
		/// </summary>
		public string SaveKey { get; private set; }
		/// <summary>
		/// The Default Value to return when calling Load() if there is no saved value found for this setting.
		/// </summary>
		public T DefaultValue { get; private set; }
		
		/// <summary>
		/// If true, the SavedSetting object will be converted to JSON before saving, and reconstructed from JSON when loaded.
		/// </summary>
		public bool JsonEncode { get; private set; }
		
		/// <summary>
		/// A function that constructs an object of type T when passed a string.
		/// <br>If this function is not null, SavedSetting.Load() will return the value of GetFromString(AppSettings[SaveKey]).</br>
		/// </summary>
		public Func<string, T> GetFromString { get; private set; }
		/// <summary>
		/// A function that constructs a string when passed an object of type T.
		/// <br>If this function is not null, SavedSetting.Save() will save the value of ConvertToString(itemT) to AppSettings[SaveKey].</br>
		/// </summary>
		public Func<T, string> ConvertToString { get; private set; }

		
		/// <summary>
		/// Hidden default constructor.
		/// </summary>
		protected SavedSetting() {}

		/// <summary>
		/// Construct a SavedSetting object that can use the Func GetFromString to construct an object to return from the string at AppSettings[SaveKey] when the Load() method is called.
		/// </summary>
		/// <param name="saveKey">The key that will be associated with the saved value in the AppSettings collection.
		/// <br>The SaveKey cannot be null, empty, or composed of only whitespace characters.</br>
		/// <br>The SaveKey must be unique, or an existing setting could be overwritten (or overwrite this setting).</br></param>
		/// <param name="defaultValue">The Default Value to return when calling Load() if there is no saved value found for this setting.</param>
		/// <param name="getFromString">A function that constructs an object of type T when passed a string.
		/// <br>If this function is not null, SavedSetting.Load() will return the value of GetFromString(AppSettings[SaveKey]).</br></param>
		public SavedSetting(string saveKey, T defaultValue, Func<string, T> getFromString, Func<T, string> convertToString)
		{

			// Do not allow a null or empty saveKey
			if (string.IsNullOrWhiteSpace(saveKey)) {
				throw new ArgumentException(
					"Tried to construct a SavedSetting object with an invalid 'saveKey' parameter. 'saveKey' parameter cannot be null or whitespace.",
					"saveKey"
				);
			}

			// Do not allow a null GetFromString Function
			if (getFromString == null) {
				throw new ArgumentException(
					"Tried to construct a SavedSetting object with an invalid 'getFromString' parameter. 'getFromString' must be a valid, non-null Func<T, string> object.",
					"getFromString"
				);
			}

			// Do not allow a null ConvertToString Function
			if (convertToString == null) {
				throw new ArgumentException(
					"Tried to construct a SavedSetting object with an invalid 'convertToString' parameter. 'convertToString' must be a valid, non-null Func<string, T> object.",
					"convertToString"
				);
			}

			SaveKey = saveKey;
			DefaultValue = defaultValue;
			JsonEncode = false;
			GetFromString = getFromString;
			ConvertToString = convertToString;

			Load();

		}

		/// <summary>
		/// Construct a SavedSetting object that uses JSON encoding for Serialization and Deserialization.
		/// </summary>
		/// <param name="saveKey">The key that will be associated with the saved value in the AppSettings collection.
		/// <br>The SaveKey cannot be null, empty, or composed of only whitespace characters.</br>
		/// <br>The SaveKey must be unique, or an existing setting could be overwritten (or overwrite this setting).</br></param>
		/// <param name="defaultValue">The Default Value to return when calling Load() if there is no saved value found for this setting.</param>
		/// <param name="saveOnValueChange">If set to true, this SavedSetting will automatically save its state to AppSettings[SaveKey] any time its Value changes. False by default.</param>
		public SavedSetting(string saveKey, T defaultValue, bool saveOnValueChange = false, bool jsonEncode = true) {
			
			// Do not allow a null or empty saveKey
			if (string.IsNullOrWhiteSpace(saveKey)) {
				throw new ArgumentException(
					"Tried to construct a SavedSetting object with an invalid 'saveKey' parameter. 'saveKey' parameter cannot be null or whitespace.", 
					"saveKey"
				);
			}

			SaveKey = saveKey; 
			DefaultValue = defaultValue;
			JsonEncode = jsonEncode;
			GetFromString = null;
			ConvertToString = null;

			Load();

			if (saveOnValueChange) {
				OnValueChanged += (o, e) => { this.Save(); };
			}

		}

		/// <summary>
		/// Load this saved setting.
		/// <br>If the string value loaded is null, the SavedSetting's DefaultValue will be returned.</br>
		/// <br>If all methods of loading fail, the default value <strong>for the type</strong> will be returned.</br>
		/// </summary>
		public void Load()
		{

			// Load the saved string, or null if none is found
			string loadedString = ConfigurationManager.AppSettings[SaveKey] ?? null;

			// No saved setting found for this key, or the saved value is null
			if (loadedString == null) { 
				Value = DefaultValue;
				Save();
				return; 
			}

			// Type T is string type, return result directly
			else if (typeof(T) == typeof(string) && !JsonEncode) {
				Value = (T)(object)loadedString;
				return;
			}

			else {
				// Construct from JSON
				if (JsonEncode) {
					try { Value = JsonConvert.DeserializeObject<SavedSetting<T>>(loadedString).Value; return; }
					catch (Exception e) { 
						Value = DefaultValue; Save(); return;
						/*App.Log($"Error: {this}.Load() Failed to deserialize from JSON.");*/
					}
				}
				// Construct from custom function
				if (GetFromString != null) {
					try { Value = GetFromString(loadedString); return; }
					catch (Exception e) { 
						Value = DefaultValue; Save(); return;
						/*App.Log($"Error: {this}.Load() Failed to construct object using GetFromString function.");*/ 
					}
				}
			}

			try { Value = DefaultValue; }
			catch { Value = default; /*All else failed, set Value to default for type T*/ }

		}

		/// <summary>
		/// Save a string representing this object at AppSettings[SaveKey]
		/// <br>If JsonEncode is set to true, the object will be converted to JSON before being saved.</br>
		/// <br>If the ConvertToString Func exists, the result of ConvertToString(Value) will be saved.</br>
		/// <br>If the item is a string, it will be saved directly.</br>
		/// </summary>
		/// <returns>True if the item is saved successfully, false if the operation encounters an exception.</returns>
		public bool Save()
		{
			try {
				if (JsonEncode) {
					App.SettingsFile.AppSettings.Settings.Remove(SaveKey);
					App.SettingsFile.AppSettings.Settings.Add(SaveKey, JsonConvert.SerializeObject(this));
				}
				else if (ConvertToString != null) {
					App.SettingsFile.AppSettings.Settings.Remove(SaveKey);
					App.SettingsFile.AppSettings.Settings.Add(SaveKey, ConvertToString(Value));
				}
				else {
					App.SettingsFile.AppSettings.Settings.Remove(SaveKey);
					App.SettingsFile.AppSettings.Settings.Add(SaveKey, Value.ToString());
				}
			}
			catch (Exception e) { 
				//App.Log($"Error: Exception in {this}.Save(): {e}");
				//return false;
			}
			
			try { App.SettingsFile.Save(ConfigurationSaveMode.Full); }
			catch (Exception e) {
				/*App.Log($"Error: Exception in {this}.Save(): {e}");*/
				return false;
			}

			return true;
		}

		/// <summary>
		/// Returns a string representation of the object in the form 'SavedSetting(SaveKey)'.
		/// </summary>
		public override string ToString()
		{
			return $"SavedSetting({SaveKey ?? INVALID_SAVEKEY})";
		}
		protected const string INVALID_SAVEKEY = "NULL_OR_EMPTY_SAVEKEY";

	}

}
