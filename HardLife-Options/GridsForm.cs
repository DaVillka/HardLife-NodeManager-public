using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HardLife_Options
{
	public partial class GridsForm : Form
	{
		private Panel _container = null!;
		private Panel _header = null!;
		private Label _titleLabel = null!;
		private Panel _content = null!;
		private Panel _footer = null!;
		private TableLayoutPanel _footerGrid = null!;

		private BufferedListView gridsListBox = null!;
		private int _hoverIndex = -1;

		private readonly Font _itemFont = new("Segoe UI", 9.5f, FontStyle.Regular);
		private readonly StringFormat _sf = new()
		{
			Alignment = StringAlignment.Near,
			LineAlignment = StringAlignment.Center,
			FormatFlags = StringFormatFlags.NoWrap,
			Trimming = StringTrimming.EllipsisCharacter
		};

		private const int ToggleBtnW = 28;
		private const int ToggleBtnH = 20;

		private static class Ui
		{
			public static readonly Color BaseBack = Color.FromArgb(35, 35, 35);
			public static readonly Color HeaderBack = Color.FromArgb(25, 25, 25);
			public static readonly Color ItemBack = Color.FromArgb(45, 45, 45);
			public static readonly Color HoverBack = Color.FromArgb(55, 55, 55);
			public static readonly Color SelectBack = Color.FromArgb(70, 70, 70);
			public static readonly Color HoverBorder = Color.FromArgb(70, 70, 70);
			public static readonly Color Accent = Color.FromArgb(0, 180, 255);

			public static readonly Color BtnBack = Color.FromArgb(45, 45, 45);
			public static readonly Color BtnBorder = Color.FromArgb(90, 90, 90);
			public static readonly Color BtnOver = Color.FromArgb(60, 60, 60);
			public static readonly Color BtnDown = Color.FromArgb(35, 35, 35);
		}

		private readonly Dictionary<string, GridItem> _gridItems = new(StringComparer.OrdinalIgnoreCase);
		private string? _selectedGridName;

		public GridsForm()
		{
			InitializeComponent();

			BuildUi();
			InitListView();
		}

		private void BuildUi()
		{
			SuspendLayout();

			_container = new Panel
			{
				Dock = DockStyle.Fill,
				BackColor = Ui.BaseBack
			};

			_header = new Panel
			{
				Dock = DockStyle.Top,
				Height = 28,
				BackColor = Ui.HeaderBack
			};

			_titleLabel = new Label
			{
				Dock = DockStyle.Fill,
				Text = "Node Property",
				ForeColor = Color.White,
				TextAlign = ContentAlignment.MiddleCenter
			};

			_content = new Panel
			{
				Dock = DockStyle.Fill,
				BackColor = Ui.BaseBack,
				Padding = new Padding(10)
			};

			_footer = new Panel
			{
				Dock = DockStyle.Bottom,
				Height = 132,
				BackColor = Ui.HeaderBack,
				Padding = new Padding(10, 8, 10, 8)
			};

			_footerGrid = new TableLayoutPanel
			{
				Dock = DockStyle.Fill,
				ColumnCount = 3,
				RowCount = 3,
				BackColor = Color.Transparent
			};

			_footerGrid.ColumnStyles.Clear();
			_footerGrid.RowStyles.Clear();

			for (int i = 0; i < _footerGrid.ColumnCount; i++)
				_footerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / _footerGrid.ColumnCount));

			for (int i = 0; i < _footerGrid.RowCount; i++)
				_footerGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / _footerGrid.RowCount));

			var btnAdd = MakeBtn("Add");
			var btnRemove = MakeBtn("Remove");
			var btnRename = MakeBtn("Rename");
			var btnSave = MakeBtn("Save");
			var btnLoad = MakeBtn("Load");
			var btnClose = MakeBtn("Close");
			var btnBuild = MakeBtn("Build");

			foreach (var btn in new[] { btnAdd, btnRemove, btnRename, btnSave, btnLoad, btnClose, btnBuild })
				btn.Dock = DockStyle.Fill;

			const int gapX = 10;
			const int gapY = 4;

			btnAdd.Margin = new Padding(0, gapY, gapX, gapY);
			btnRemove.Margin = new Padding(0, gapY, gapX, gapY);
			btnRename.Margin = new Padding(0, gapY, 0, gapY);
			btnSave.Margin = new Padding(0, gapY, gapX, gapY);
			btnLoad.Margin = new Padding(0, gapY, gapX, gapY);
			btnClose.Margin = new Padding(0, gapY, 0, gapY);
			btnBuild.Margin = new Padding(0, gapY, 0, gapY);

			static void TintBtn(Button btn, Color back, Color border)
			{
				btn.BackColor = back;
				btn.FlatAppearance.BorderColor = border;
				btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(back, 0.08f);
				btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(back, 0.12f);
			}

			TintBtn(btnAdd, Color.FromArgb(42, 62, 46), Color.FromArgb(95, 150, 110));
			TintBtn(btnRemove, Color.FromArgb(66, 42, 42), Color.FromArgb(170, 90, 90));
			TintBtn(btnRename, Color.FromArgb(62, 56, 42), Color.FromArgb(175, 150, 95));
			TintBtn(btnSave, Color.FromArgb(42, 54, 66), Color.FromArgb(105, 145, 175));
			TintBtn(btnLoad, Color.FromArgb(56, 42, 66), Color.FromArgb(150, 115, 175));
			TintBtn(btnClose, Ui.BtnBack, Ui.BtnBorder);
			TintBtn(btnBuild, Color.FromArgb(35, 58, 70), Ui.Accent);

			_footerGrid.Controls.Add(btnAdd, 0, 0);
			_footerGrid.Controls.Add(btnRemove, 1, 0);
			_footerGrid.Controls.Add(btnRename, 2, 0);
			_footerGrid.Controls.Add(btnSave, 0, 1);
			_footerGrid.Controls.Add(btnLoad, 1, 1);
			_footerGrid.Controls.Add(btnClose, 2, 1);
			_footerGrid.Controls.Add(btnBuild, 0, 2);
			_footerGrid.SetColumnSpan(btnBuild, _footerGrid.ColumnCount);


			_header.Controls.Add(_titleLabel);
			_footer.Controls.Add(_footerGrid);

			_container.Controls.Add(_content);
			_container.Controls.Add(_footer);
			_container.Controls.Add(_header);

			Controls.Add(_container);

			ResumeLayout(true);
		}

		private Button MakeBtn(string text)
		{
			var b = new Button
			{
				Text = text,
				Width = 110,
				Height = 30,
				Margin = Padding.Empty,
				FlatStyle = FlatStyle.Flat,
				UseVisualStyleBackColor = false,
				ForeColor = Color.White,
				BackColor = Ui.BtnBack,
				Cursor = Cursors.Hand,
				Font = new Font("Segoe UI", 9.0f, FontStyle.Regular),
				TabStop = false
			};

			b.FlatAppearance.BorderSize = 1;
			b.FlatAppearance.BorderColor = Ui.BtnBorder;
			b.FlatAppearance.MouseOverBackColor = Ui.BtnOver;
			b.FlatAppearance.MouseDownBackColor = Ui.BtnDown;

			b.Click += OnButtonClicked;
			return b;
		}

		private void InitListView()
		{
			gridsListBox = new BufferedListView
			{
				Dock = DockStyle.Fill,
				BackColor = Ui.BaseBack,
				BorderStyle = BorderStyle.None,
				ForeColor = Color.Gainsboro,
				MultiSelect = false,
				FullRowSelect = true,
				HideSelection = false,
				HeaderStyle = ColumnHeaderStyle.None,
				View = View.Details,
				OwnerDraw = true
			};

			gridsListBox.Columns.Clear();
			gridsListBox.Columns.Add("", -2, HorizontalAlignment.Left);

			gridsListBox.SmallImageList ??= new ImageList();
			gridsListBox.SmallImageList.ImageSize = new Size(1, 32);

			gridsListBox.Resize += GridsListView_Resize;
			gridsListBox.MouseMove += GridsListView_MouseMove;
			gridsListBox.MouseLeave += GridsListView_MouseLeave;
			gridsListBox.MouseDown += GridsListView_MouseDown;

			gridsListBox.DrawColumnHeader += GridsListView_DrawColumnHeader;
			gridsListBox.DrawItem += GridsListView_DrawItem;

			gridsListBox.SelectedIndexChanged += SelectedGridChanged;

			GridsListView_Resize(this, EventArgs.Empty);

			_content.Controls.Add(gridsListBox);
			_hoverIndex = -1;
		}

		private void GridsListView_Resize(object? sender, EventArgs e)
		{
			if (gridsListBox.Columns.Count > 0)
				gridsListBox.Columns[0].Width = gridsListBox.ClientSize.Width - 2;
		}

		private void GridsListView_MouseMove(object? sender, MouseEventArgs e)
		{
			var item = gridsListBox.GetItemAt(e.X, e.Y);
			int idx = item?.Index ?? -1;

			if (idx == _hoverIndex) return;
			_hoverIndex = idx;
			gridsListBox.Invalidate();
		}

		private void GridsListView_MouseLeave(object? sender, EventArgs e)
		{
			if (_hoverIndex == -1) return;
			_hoverIndex = -1;
			gridsListBox.Invalidate();
		}

		private Rectangle GetToggleRect(Rectangle itemBounds)
		{
			var rect = new Rectangle(itemBounds.X + 8, itemBounds.Y + 4, itemBounds.Width - 16, itemBounds.Height - 8);

			int x = rect.Right - ToggleBtnW - 6;
			int y = rect.Top + (rect.Height - ToggleBtnH) / 2;

			return new Rectangle(x, y, ToggleBtnW, ToggleBtnH);
		}

		private void GridsListView_MouseDown(object? sender, MouseEventArgs e)
		{
			var item = gridsListBox.GetItemAt(e.X, e.Y);

			if (item == null)
			{
				gridsListBox.SelectedIndices.Clear();
				gridsListBox.Invalidate();
				return;
			}

			var toggleRect = GetToggleRect(item.Bounds);

			if (toggleRect.Contains(e.Location))
			{
				var name = (item.Text ?? "").Trim();
				ToggleGridVisibility(name);
				gridsListBox.Invalidate();
				return;
			}

			if (e.Button == MouseButtons.Left && item.Selected)
			{
				var name = (item.Text ?? "").Trim();
				if (name.Length > 0)
				{
					_selectedGridName = name;
					ShowGrid(name, activate: true);
					gridsListBox.Invalidate();
				}
			}
		}

		private void GridsListView_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = false;
		}

		private void GridsListView_DrawItem(object? sender, DrawListViewItemEventArgs e)
		{
			var g = e.Graphics;
			g.SmoothingMode = SmoothingMode.None;

			bool selected = e.Item.Selected;
			bool hovered = e.Item.Index == _hoverIndex;

			var bounds = e.Bounds;

			using (var bg = new SolidBrush(Ui.BaseBack))
				g.FillRectangle(bg, bounds);

			var rect = new Rectangle(bounds.X + 8, bounds.Y + 4, bounds.Width - 16, bounds.Height - 8);
			Color fill = selected ? Ui.SelectBack : hovered ? Ui.HoverBack : Ui.ItemBack;

			using (var br = new SolidBrush(fill))
				g.FillRectangle(br, rect);

			if (selected)
			{
				var accentRect = new Rectangle(rect.Left + 2, rect.Top + 4, 4, rect.Height - 8);
				using var abr = new SolidBrush(Ui.Accent);
				g.FillRectangle(abr, accentRect);
			}

			var name = (e.Item.Text ?? string.Empty).Trim();
			bool isVisible = _gridItems.TryGetValue(name, out var gi) && gi.IsVisible;

			var toggleRect = GetToggleRect(bounds);

			var textRect = new Rectangle(rect.Left + 14, rect.Top, rect.Width - 20 - (ToggleBtnW + 10), rect.Height);
			using var textBrush = new SolidBrush(Color.White);
			g.DrawString(name, _itemFont, textBrush, textRect, _sf);

			Color btnFill = isVisible ? Color.FromArgb(35, 110, 35) : Color.FromArgb(85, 35, 35);
			Color btnBorder = Color.FromArgb(90, 90, 90);
			string btnText = isVisible ? "—" : "◻";

			using (var bbr = new SolidBrush(btnFill))
				g.FillRectangle(bbr, toggleRect);

			using (var pen = new Pen(btnBorder))
				g.DrawRectangle(pen, toggleRect);

			using (var f = new Font("Segoe UI", 9f, FontStyle.Bold))
			using (var tbr = new SolidBrush(Color.White))
			{
				var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
				g.DrawString(btnText, f, tbr, toggleRect, sf);
			}

			if (hovered && !selected)
			{
				using var pen2 = new Pen(Ui.HoverBorder);
				g.DrawRectangle(pen2, rect);
			}
		}

		public void SetGrids(IEnumerable<string> grids, bool replace = false)
		{
			if (grids is null) throw new ArgumentNullException(nameof(grids));

			var list = grids
				.Select(x => x?.Trim())
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToList();

			gridsListBox.BeginUpdate();
			try
			{
				if (replace)
				{
					gridsListBox.Items.Clear();
					CloseAllGridForms();
					_gridItems.Clear();
					_selectedGridName = null;
				}

				foreach (var name in list)
				{
					if (!ListContains(name))
						gridsListBox.Items.Add(new ListViewItem(name));

					if (!_gridItems.ContainsKey(name))
					{
						var gi = new GridItem(null, isVisible: false);
						_gridItems[name] = gi;
						GetOrCreateForm(name, gi);
					}
				}
			}
			finally
			{
				gridsListBox.EndUpdate();
			}

			CleanupOrphanedGridItems();
			gridsListBox.Invalidate();
		}

		public void ClearGrids()
		{
			gridsListBox.Items.Clear();
			CloseAllGridForms();
			_gridItems.Clear();
			_selectedGridName = null;
		}

		private bool ListContains(string name)
		{
			foreach (ListViewItem it in gridsListBox.Items)
			{
				if (string.Equals(it.Text, name, StringComparison.OrdinalIgnoreCase))
					return true;
			}
			return false;
		}

		private HashSet<string> GetNamesFromList()
		{
			var hs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (ListViewItem it in gridsListBox.Items)
			{
				var t = (it.Text ?? "").Trim();
				if (t.Length > 0) hs.Add(t);
			}
			return hs;
		}

		private void CleanupOrphanedGridItems()
		{
			var names = GetNamesFromList();
			var toRemove = new List<string>();

			foreach (var k in _gridItems.Keys)
			{
				if (!names.Contains(k))
					toRemove.Add(k);
			}

			foreach (var k in toRemove)
			{
				CloseGridForm(k);
				_gridItems.Remove(k);
			}
		}

		private void CloseAllGridForms()
		{
			foreach (var kv in _gridItems)
			{
				var f = kv.Value.Form;
				if (f != null && !f.IsDisposed)
				{
					try { f.Close(); } catch { }
					try { f.Dispose(); } catch { }
				}
				kv.Value.Form = null;
				kv.Value.IsVisible = false;
			}
		}

		private void CloseGridForm(string name)
		{
			if (_gridItems.TryGetValue(name, out var item))
			{
				var f = item.Form;
				if (f != null && !f.IsDisposed)
				{
					try { f.Close(); } catch { }
					try { f.Dispose(); } catch { }
				}
				item.Form = null;
				item.IsVisible = false;
			}
		}

		private void SelectedGridChanged(object? sender, EventArgs e)
		{
			var name = GetSelectedGridName();
			_selectedGridName = name;
			gridsListBox.Invalidate();
		}

		private string? GetSelectedGridName()
		{
			if (gridsListBox.SelectedItems.Count <= 0) return null;
			return (gridsListBox.SelectedItems[0].Text ?? "").Trim();
		}

		private GridItem GetOrCreateGridItem(string name)
		{
			if (!_gridItems.TryGetValue(name, out var gi))
			{
				gi = new GridItem(null, isVisible: false);
				_gridItems[name] = gi;
			}
			return gi;
		}

		private MainForm GetOrCreateForm(string name, GridItem gi)
		{
			if (gi.Form != null && !gi.Form.IsDisposed)
				return gi.Form;

			if (gi.Form != null && gi.Form.IsDisposed)
				gi.Form = null;

			var f = new MainForm
			{
				Text = name,
				ShowInTaskbar = false
			};

			f.FormClosed += (_, __) =>
			{
				if (_gridItems.TryGetValue(name, out var x))
					x.IsVisible = false;
				

				if (string.Equals(_selectedGridName, name, StringComparison.OrdinalIgnoreCase))
					_selectedGridName = null;

				gridsListBox.Invalidate();
			};

			gi.Form = f;
			return f;
		}

		private void ShowGrid(string name, bool activate)
		{
			var gi = GetOrCreateGridItem(name);
			var f = GetOrCreateForm(name, gi);

			if (f.WindowState == FormWindowState.Minimized)
				f.WindowState = FormWindowState.Normal;

			if (!f.Visible)
			{
				int xPos = this.Right + 10;
				int yPos = this.Top;
				f.StartPosition = FormStartPosition.Manual;
				f.Location = new Point(xPos, yPos);
				f.Show(this);
			}

			gi.IsVisible = true;

			if (activate)
			{
				f.BringToFront();
				f.Activate();
			}
		}

		private void HideGrid(string name)
		{
			if (!_gridItems.TryGetValue(name, out var gi)) return;

			var f = gi.Form;
			if (f != null && !f.IsDisposed && f.Visible)
				f.Hide();

			gi.IsVisible = false;
		}

		private void ToggleGridVisibility(string name)
		{
			var gi = GetOrCreateGridItem(name);

			if (gi.IsVisible)
				HideGrid(name);
			else
				ShowGrid(name, activate: false);
		}

		private void OnButtonClicked(object? sender, EventArgs e)
		{
			if (sender is not Button btn) return;

			switch (btn.Text)
			{
				case "Add":
					AddGrid();
					break;

				case "Remove":
					RemoveSelectedGrid();
					break;

				case "Rename":
					RenameSelectedGrid();
					break;

				case "Save":
					SaveGridsToFile();
					break;

				case "Load":
					LoadGridsFromFile();
					break;

				case "Close":
					Close();
					break;
			}
		}

		private void AddGrid()
		{
			var baseName = "Grid";
			int n = gridsListBox.Items.Count + 1;

			string name;
			do
			{
				name = $"{baseName} {n}";
				n++;
			} while (ListContains(name));

			gridsListBox.Items.Add(new ListViewItem(name));
			GetOrCreateGridItem(name).IsVisible = false;

			gridsListBox.SelectedIndices.Clear();
			gridsListBox.EnsureVisible(gridsListBox.Items.Count - 1);
		}

		private void RemoveSelectedGrid()
		{
			var name = GetSelectedGridName();
			if (string.IsNullOrWhiteSpace(name)) return;

			CloseGridForm(name);
			_gridItems.Remove(name);

			gridsListBox.BeginUpdate();
			try
			{
				if (gridsListBox.SelectedItems.Count > 0)
					gridsListBox.Items.Remove(gridsListBox.SelectedItems[0]);
			}
			finally
			{
				gridsListBox.EndUpdate();
			}

			_selectedGridName = null;
			gridsListBox.Invalidate();
		}

		private void RenameSelectedGrid()
		{
			var oldName = GetSelectedGridName();
			if (string.IsNullOrWhiteSpace(oldName)) return;

			var dialog = new Form
			{
				Text = "Rename Grid",
				Width = 350,
				Height = 150,
				StartPosition = FormStartPosition.CenterParent,
				FormBorderStyle = FormBorderStyle.FixedDialog,
				MaximizeBox = false,
				MinimizeBox = false,
				BackColor = Ui.BaseBack,
				ForeColor = Color.White
			};

			var label = new Label
			{
				Text = "New name:",
				Left = 20,
				Top = 20,
				AutoSize = true,
				ForeColor = Color.White
			};

			var textBox = new TextBox
			{
				Text = oldName,
				Left = 20,
				Top = 45,
				Width = 310,
				BackColor = Ui.ItemBack,
				ForeColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle
			};

			var btnOk = new Button
			{
				Text = "OK",
				Left = 160,
				Top = 80,
				Width = 75,
				Height = 30,
				BackColor = Ui.BtnBack,
				ForeColor = Color.White,
				FlatStyle = FlatStyle.Flat,
				DialogResult = DialogResult.OK
			};
			btnOk.FlatAppearance.BorderColor = Ui.BtnBorder;

			var btnCancel = new Button
			{
				Text = "Cancel",
				Left = 245,
				Top = 80,
				Width = 75,
				Height = 30,
				BackColor = Ui.BtnBack,
				ForeColor = Color.White,
				FlatStyle = FlatStyle.Flat,
				DialogResult = DialogResult.Cancel
			};
			btnCancel.FlatAppearance.BorderColor = Ui.BtnBorder;

			dialog.Controls.Add(label);
			dialog.Controls.Add(textBox);
			dialog.Controls.Add(btnOk);
			dialog.Controls.Add(btnCancel);

			if (dialog.ShowDialog(this) != DialogResult.OK)
				return;

			var newName = textBox.Text?.Trim();

			if (string.IsNullOrWhiteSpace(newName))
			{
				MessageBox.Show("Grid name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (string.Equals(newName, oldName, StringComparison.OrdinalIgnoreCase))
				return;

			if (ListContains(newName))
			{
				MessageBox.Show("A grid with this name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			var selectedItem = gridsListBox.SelectedItems[0];
			selectedItem.Text = newName;

			if (_gridItems.TryGetValue(oldName, out var item))
			{
				_gridItems.Remove(oldName);
				_gridItems[newName] = item;

				if (item.Form != null && !item.Form.IsDisposed)
					item.Form.Text = newName;
			}

			_selectedGridName = newName;
			gridsListBox.Invalidate();
		}

		private void SaveGridsToFile()
		{
			var saveData = new Dictionary<string, object>();
			var gridsData = new List<Dictionary<string, object>>();

			string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "save");
			string filePath = Path.Combine(directoryPath, "grids_data.json");

			Directory.CreateDirectory(directoryPath);
			foreach (ListViewItem item in gridsListBox.Items)
			{
				var gridName = item.Text?.Trim();
				if (string.IsNullOrWhiteSpace(gridName)) continue;

				if (_gridItems.TryGetValue(gridName, out var gridItem))
				{
					var form = GetOrCreateForm(gridName, gridItem);
					bool isVisible = form.Visible;
					gridItem.IsVisible = isVisible;

					var gridDataItem = new Dictionary<string, object>
					{
						{ "name", gridName },
						{ "isVisible", isVisible },
						{ "data", form.SaveGridData(directoryPath, gridName) }
					};
					gridsData.Add(gridDataItem);
				}
			}

			saveData["grids"] = gridsData;

			try
			{
				

				string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
				File.WriteAllText(filePath, json);

				MessageBox.Show("Grids saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error saving grids: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void LoadGridsFromFile()
		{
			try
			{
				string filePath = "save/grids_data.json";
				if (!System.IO.File.Exists(filePath))
				{
					MessageBox.Show("No saved grids file found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				string json = System.IO.File.ReadAllText(filePath);
				var saveData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

				if (saveData == null || !saveData.TryGetValue("grids", out var gridsObj))
					return;

                if (gridsObj is not Newtonsoft.Json.Linq.JArray gridsArray) return;

                gridsListBox.Items.Clear();
				CloseAllGridForms();
				_gridItems.Clear();

				var gridsToOpen = new List<string>();
				foreach (var gridObj in gridsArray)
				{
					var gridDict = gridObj as Newtonsoft.Json.Linq.JObject;
					if (gridDict == null) continue;

					string? gridName = gridDict.Value<string>("name");
					bool wasVisible = gridDict.Value<bool?>("isVisible") ?? false;

					if (string.IsNullOrWhiteSpace(gridName)) continue;

					var item = new ListViewItem(gridName);
					gridsListBox.Items.Add(item);

					var gridItem = new GridItem(null, false);
					_gridItems[gridName] = gridItem;

					if (gridDict.TryGetValue("data", out var gridData))
					{
					    var form = GetOrCreateForm(gridName, gridItem);
					    form.LoadGridData(gridData);
					}

					if (wasVisible)
						gridsToOpen.Add(gridName);
				}

				foreach (var name in gridsToOpen)
					ShowGrid(name, activate: false);

				gridsListBox.Invalidate();
				MessageBox.Show("Grids loaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error loading grids: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public class BufferedListView : ListView
		{
			public BufferedListView()
			{
				DoubleBuffered = true;
				SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
				UpdateStyles();
			}

			protected override CreateParams CreateParams
			{
				get
				{
					var cp = base.CreateParams;
					cp.ExStyle |= 0x02000000;
					return cp;
				}
			}
		}

		private sealed class GridItem
		{
			public MainForm? Form { get; set; }
			public bool IsVisible { get; set; }

			public GridItem(MainForm? form = null, bool isVisible = false)
			{
				Form = form;
				IsVisible = isVisible;
			}
		}
	}
}
