using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections;

namespace RconTool
{

	/// <summary>
	/// A list that's saved as a JSON file in AppData.
	/// <br>The file is updated efficiently each time an item is added.</br>
	/// </summary>
	public class SavedRecord<T> : IList<T>
	{

		private List<T> list = new List<T>();

		public FileInfo SaveFile { get; set; }

		// This parameterless constructor is bad
		// and nobody should be allowed to use it
		private SavedRecord() {}

		public SavedRecord(string saveKey, T defaultValue) {
			if (string.IsNullOrWhiteSpace(saveKey)) {
				throw new ArgumentNullException(nameof(saveKey));
			}
			if (saveKey.IndexOfAny(Path.GetInvalidFileNameChars()) > -1) {
				throw new ArgumentException("saveKey contained invalid filename characters.", nameof(saveKey));
			}

			// Determine base directory relative to the running executable.
			string baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SavedRecords");

			// Validate or create the directory.
			if (!Directory.Exists(baseDir)) {
				try {
					Directory.CreateDirectory(baseDir);
				}
				catch (Exception dirEx) {
					App.Error("Directory Creation Failed",
						$"Failed to create or access directory:\n{baseDir}\n\nException Info:\n{dirEx}");
					SaveFile = null;
					return;
				}
			}

			// Construct the file path in the validated directory.
			string filePath = Path.Combine(baseDir, $"{saveKey}.json");
			SaveFile = new FileInfo(filePath);

			// Load or create file
			if (SaveFile.Exists) {
				Load();
			}
			else {
				try {
					using (StreamWriter file = SaveFile.CreateText()) {
						file.WriteLine("[");
						file.Write("]");
					}
				}
				catch (Exception e) {
					App.Error("Save Failed", $"Failed to save data to:\n{SaveFile.FullName}\n\nException Info:\n{e}");
				}
			}
		}

		/// <summary>
		/// Deserialize JSON directly from save file. Returns true if successful.
		/// </summary>
		public bool Load()
		{
			if (SaveFile?.Exists ?? false) {
				try {
					using (StreamReader file = SaveFile.OpenText()) {
						JsonSerializer serializer = new JsonSerializer();
						list = (List<T>)serializer.Deserialize(file, typeof(List<T>));
					}
				}
				catch (Exception e) {
					App.Error("Load Failed", $"Failed to load data from:\n{SaveFile.FullName}\n\nException Info:\n{e}");
					list = new List<T>(); return false; 
				}
				return true;
			}
			return false;
		}

		#region IList Implementation

		public int Count => ((ICollection<T>)list).Count;

		public bool IsReadOnly => ((ICollection<T>)list).IsReadOnly;

		public T this[int index] { get => ((IList<T>)list)[index]; set => ((IList<T>)list)[index] = value; }

		public int IndexOf(T item)
		{
			return ((IList<T>)list).IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			((IList<T>)list).Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			((IList<T>)list).RemoveAt(index);
		}

		public void Add(T item)
		{
			((ICollection<T>)list).Add(item);
			// Update SavedRecord
			try {
				unsavedItems.Enqueue(item); byte[] itemBytes;
				using (FileStream fs = SaveFile.OpenWrite()) {
					while (unsavedItems.Count > 0) {
						try {
							fs.Seek(-1, SeekOrigin.End);
							itemBytes = Encoding.UTF8.GetBytes($"\t{JsonConvert.SerializeObject(unsavedItems.Peek())},\n]");
							fs.Write(itemBytes, 0, itemBytes.Length);
							unsavedItems.Dequeue();
						}
						catch { break; }
					}
					// Only needed if new content may be smaller than old
					// fs.SetLength(fs.Position);
				}
			}
			catch (Exception e) { 
				App.Log($"SavedRecord.Add Exception. Error updating saved record:\n{e}"); 
			}
		}
		private Queue<T> unsavedItems = new Queue<T>();

		public void Clear()
		{
			((ICollection<T>)list).Clear();
		}

		public bool Contains(T item)
		{
			return ((ICollection<T>)list).Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			((ICollection<T>)list).CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return ((ICollection<T>)list).Remove(item);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)list).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)list).GetEnumerator();
		}

		#endregion

	}

}
