# Design Document

## Overview

This design document outlines the technical approach for creating comprehensive XAML views for the AquaPP Industrial IoT Monitoring Dashboard. The implementation will leverage SukiUI 6.0.3 components for consistent theming, Avalonia UI 11.3.8 for cross-platform rendering, and follow MVVM patterns established in the existing codebase. All views will be created in the `AquaPP/Views/Pages/` directory with corresponding ViewModels in `AquaPP/ViewModels/Pages/`.

## Architecture

### View Structure

```
AquaPP/Views/Pages/
├── MonitoringDashboardView.axaml          # Real-time monitoring overview
├── SensorDetailView.axaml                 # Individual sensor details with charts
├── AlertManagementView.axaml              # Alert history and management
├── ThresholdConfigurationView.axaml       # Threshold settings editor
├── SensorListView.axaml                   # All sensors list view
├── AlertConfigurationView.axaml           # Alert notification settings
├── DataExportView.axaml                   # Historical data export
├── AssetDirectoryView.axaml               # Enhanced (already exists)
├── SettingsView.axaml                     # Enhanced (already exists)
└── DashboardView.axaml                    # Enhanced (already exists)
```

### Component Hierarchy

```
SukiWindow (MainWindowView)
└── SukiSideMenu
    └── ContentPresenter (Page Views)
        ├── MonitoringDashboardView
        │   └── Grid of GlassCards (Asset Status Cards)
        ├── SensorDetailView
        │   ├── GlassCard (Sensor Info)
        │   ├── GlassCard (Current Value Gauge)
        │   └── GlassCard (Chart Container)
        ├── AlertManagementView
        │   ├── GlassCard (Filters)
        │   └── GlassCard (DataGrid)
        └── [Other Views...]
```

## Components and Interfaces

### 1. MonitoringDashboardView

**Purpose:** Primary real-time monitoring interface displaying all assets and key metrics.

**Layout Structure:**
- Header section with KPI cards (Total Assets, Online Sensors, Active Alerts, System Uptime)
- Grid layout (responsive columns) for asset status cards
- Each card shows: Asset name, status indicator, key sensor values, mini trend sparkline

**SukiUI Components:**
- `GlassCard` for KPI cards and asset cards
- `Icon` (FontAwesome) for status indicators and metrics
- `Border` with `CornerRadius` for card styling

**Data Binding:**
- ViewModel: `MonitoringDashboardViewModel`
- Properties: `Assets`, `TotalAssets`, `OnlineSensors`, `ActiveAlerts`, `SystemUptime`
- Commands: `RefreshCommand`, `NavigateToAssetCommand`

**XAML Pattern:**
```xml
<ScrollViewer>
    <StackPanel Spacing="20" Margin="20">
        <!-- KPI Section -->
        <Grid ColumnDefinitions="*,*,*,*" ColumnSpacing="15">
            <suki:GlassCard Grid.Column="0">
                <!-- Total Assets KPI -->
            </suki:GlassCard>
            <!-- More KPI cards -->
        </Grid>
        
        <!-- Asset Cards Grid -->
        <ItemsControl ItemsSource="{Binding Assets}">
            <ItemsControl.ItemsPanel>
                <ItemsPanelTemplate>
                    <WrapPanel Orientation="Horizontal" />
                </ItemsPanelTemplate>
            </ItemsControl.ItemsPanel>
            <ItemsControl.ItemTemplate>
                <DataTemplate>
                    <suki:GlassCard Width="300" Height="200">
                        <!-- Asset card content -->
                    </suki:GlassCard>
                </DataTemplate>
            </ItemsControl.ItemTemplate>
        </ItemsControl>
    </StackPanel>
</ScrollViewer>
```

### 2. SensorDetailView

**Purpose:** Detailed view of individual sensor with real-time chart and statistics.

**Layout Structure:**
- Top section: Sensor metadata (name, type, unit, asset, status)
- Middle section: Large current value display with gauge/progress indicator
- Statistics row: Min, Max, Average, Last Update
- Bottom section: Real-time line chart with time window selector
- Chart controls: Zoom, Pan, Export

**SukiUI Components:**
- `GlassCard` for each section
- `ComboBox` for time window selection
- `ProgressBar` styled as gauge for current value
- Chart placeholder (ScottPlot.Avalonia to be integrated in Phase 2)

**Data Binding:**
- ViewModel: `SensorDetailViewModel`
- Properties: `Sensor`, `CurrentValue`, `MinValue`, `MaxValue`, `AvgValue`, `ChartData`, `SelectedTimeWindow`
- Commands: `ChangeTimeWindowCommand`, `ExportChartCommand`, `ZoomInCommand`, `ZoomOutCommand`

**XAML Pattern:**
```xml
<Grid RowDefinitions="Auto,Auto,*,Auto" Spacing="20" Margin="20">
    <!-- Sensor Info Card -->
    <suki:GlassCard Grid.Row="0">
        <Grid ColumnDefinitions="*,*,*,*">
            <!-- Sensor metadata -->
        </Grid>
    </suki:GlassCard>
    
    <!-- Current Value Card -->
    <suki:GlassCard Grid.Row="1">
        <StackPanel HorizontalAlignment="Center">
            <TextBlock Text="{Binding CurrentValue}" FontSize="48" />
            <ProgressBar Value="{Binding CurrentValuePercent}" />
        </StackPanel>
    </suki:GlassCard>
    
    <!-- Chart Card -->
    <suki:GlassCard Grid.Row="2">
        <!-- Chart control placeholder -->
        <Border Background="{DynamicResource SukiBackground}">
            <TextBlock Text="Chart will be rendered here" />
        </Border>
    </suki:GlassCard>
    
    <!-- Chart Controls -->
    <suki:GlassCard Grid.Row="3">
        <StackPanel Orientation="Horizontal" Spacing="10">
            <ComboBox ItemsSource="{Binding TimeWindows}" />
            <Button Content="Export" />
        </StackPanel>
    </suki:GlassCard>
</Grid>
```

### 3. AlertManagementView

**Purpose:** View and manage all system alerts with filtering and acknowledgment.

**Layout Structure:**
- Header with unacknowledged alert count badge
- Filter panel: Date range pickers, asset selector, severity selector
- DataGrid with columns: Timestamp, Asset, Sensor, Severity, Message, Status, Actions
- Selected alert detail panel at bottom
- Bulk action buttons: Acknowledge Selected, Export

**SukiUI Components:**
- `GlassCard` for filter panel and detail panel
- `DatePicker` for date range
- `ComboBox` for dropdowns
- `DataGrid` with custom cell templates
- `Button` with icon for actions

**Data Binding:**
- ViewModel: `AlertManagementViewModel`
- Properties: `Alerts`, `FilteredAlerts`, `SelectedAlert`, `UnacknowledgedCount`, `StartDate`, `EndDate`, `SelectedAsset`, `SelectedSeverity`
- Commands: `AcknowledgeCommand`, `AcknowledgeAllCommand`, `ExportCommand`, `ApplyFiltersCommand`, `ClearFiltersCommand`

**XAML Pattern:**
```xml
<Grid RowDefinitions="Auto,Auto,*,Auto" Spacing="20" Margin="20">
    <!-- Header -->
    <StackPanel Grid.Row="0" Orientation="Horizontal" Spacing="10">
        <TextBlock Text="Alert Management" FontSize="28" FontWeight="Bold" />
        <Border Background="#F44336" CornerRadius="12" Padding="8,4">
            <TextBlock Text="{Binding UnacknowledgedCount}" Foreground="White" />
        </Border>
    </StackPanel>
    
    <!-- Filters -->
    <suki:GlassCard Grid.Row="1">
        <Grid ColumnDefinitions="*,*,*,Auto" ColumnSpacing="10">
            <DatePicker Grid.Column="0" />
            <ComboBox Grid.Column="1" />
            <ComboBox Grid.Column="2" />
            <Button Grid.Column="3" Content="Apply" />
        </Grid>
    </suki:GlassCard>
    
    <!-- DataGrid -->
    <suki:GlassCard Grid.Row="2">
        <DataGrid ItemsSource="{Binding FilteredAlerts}" />
    </suki:GlassCard>
    
    <!-- Detail Panel -->
    <suki:GlassCard Grid.Row="3" IsVisible="{Binding SelectedAlert, Converter={x:Static ObjectConverters.IsNotNull}}">
        <!-- Alert details -->
    </suki:GlassCard>
</Grid>
```

### 4. ThresholdConfigurationView

**Purpose:** Configure sensor thresholds with validation and preview.

**Layout Structure:**
- Search/filter bar for sensors
- DataGrid with columns: Sensor Name, Type, Current Min, Current Max, New Min, New Max, Actions
- Inline editing with NumericUpDown controls
- Validation indicators (red border for invalid values)
- Preview panel showing impact on historical data
- Bulk update section for sensor type groups

**SukiUI Components:**
- `GlassCard` for sections
- `TextBox` for search
- `DataGrid` with editable cells
- `NumericUpDown` for threshold values
- `Button` for save/cancel actions

**Data Binding:**
- ViewModel: `ThresholdConfigurationViewModel`
- Properties: `Sensors`, `FilteredSensors`, `SelectedSensor`, `HasUnsavedChanges`, `ValidationErrors`
- Commands: `SaveCommand`, `CancelCommand`, `BulkUpdateCommand`, `PreviewCommand`, `ResetCommand`

**XAML Pattern:**
```xml
<Grid RowDefinitions="Auto,Auto,*,Auto" Spacing="20" Margin="20">
    <!-- Header with Search -->
    <suki:GlassCard Grid.Row="0">
        <Grid ColumnDefinitions="*,Auto">
            <TextBox Grid.Column="0" Watermark="Search sensors..." />
            <Button Grid.Column="1" Content="Bulk Update" />
        </Grid>
    </suki:GlassCard>
    
    <!-- Threshold Grid -->
    <suki:GlassCard Grid.Row="2">
        <DataGrid ItemsSource="{Binding FilteredSensors}" CanUserReorderColumns="True">
            <DataGrid.Columns>
                <DataGridTextColumn Header="Sensor" Binding="{Binding Name}" />
                <DataGridTemplateColumn Header="Min Threshold">
                    <DataGridTemplateColumn.CellTemplate>
                        <DataTemplate>
                            <NumericUpDown Value="{Binding MinThreshold}" />
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>
                <!-- More columns -->
            </DataGrid.Columns>
        </DataGrid>
    </suki:GlassCard>
    
    <!-- Action Buttons -->
    <suki:GlassCard Grid.Row="3">
        <StackPanel Orientation="Horizontal" Spacing="10" HorizontalAlignment="Right">
            <Button Content="Cancel" Command="{Binding CancelCommand}" />
            <Button Content="Save Changes" Command="{Binding SaveCommand}" Classes="Accent" />
        </StackPanel>
    </suki:GlassCard>
</Grid>
```

### 5. SensorListView

**Purpose:** Comprehensive list of all sensors with grouping and filtering.

**Layout Structure:**
- Header with sensor count and status summary
- Filter/group controls: Group by (Asset/Type/Status), Status filter
- DataGrid with columns: Status, Name, Type, Current Value, Unit, Asset, Last Update
- Real-time value updates when streaming active
- Click row to navigate to sensor detail

**SukiUI Components:**
- `GlassCard` for header and grid container
- `ComboBox` for grouping options
- `DataGrid` with custom templates
- Status indicator ellipses with color binding

**Data Binding:**
- ViewModel: `SensorListViewModel`
- Properties: `Sensors`, `FilteredSensors`, `GroupBy`, `StatusFilter`, `IsStreaming`, `TotalSensors`, `OnlineSensors`
- Commands: `NavigateToDetailCommand`, `RefreshCommand`, `ToggleStreamingCommand`

**XAML Pattern:**
```xml
<Grid RowDefinitions="Auto,Auto,*" Spacing="20" Margin="20">
    <!-- Header with Stats -->
    <suki:GlassCard Grid.Row="0">
        <Grid ColumnDefinitions="*,*,*">
            <StackPanel Grid.Column="0">
                <TextBlock Text="Total Sensors" />
                <TextBlock Text="{Binding TotalSensors}" FontSize="24" />
            </StackPanel>
            <!-- More stats -->
        </Grid>
    </suki:GlassCard>
    
    <!-- Filters -->
    <suki:GlassCard Grid.Row="1">
        <StackPanel Orientation="Horizontal" Spacing="10">
            <TextBlock Text="Group by:" VerticalAlignment="Center" />
            <ComboBox ItemsSource="{Binding GroupOptions}" />
            <TextBlock Text="Status:" VerticalAlignment="Center" />
            <ComboBox ItemsSource="{Binding StatusOptions}" />
        </StackPanel>
    </suki:GlassCard>
    
    <!-- Sensor Grid -->
    <suki:GlassCard Grid.Row="2">
        <DataGrid ItemsSource="{Binding FilteredSensors}" />
    </suki:GlassCard>
</Grid>
```

### 6. AlertConfigurationView

**Purpose:** Configure alert notification settings and test notifications.

**Layout Structure:**
- Notification channels section with toggle switches
- Severity level configuration for each channel
- Sound selection dropdown
- Test notification button
- Notification history preview

**SukiUI Components:**
- `GlassCard` for each settings section
- `ToggleSwitch` for enable/disable
- `ComboBox` for severity and sound selection
- `Button` for test action
- `Separator` for visual grouping

**Data Binding:**
- ViewModel: `AlertConfigurationViewModel`
- Properties: `DesktopToastEnabled`, `SoundEnabled`, `InAppEnabled`, `SelectedSeverityLevel`, `SelectedSound`
- Commands: `TestNotificationCommand`, `SaveCommand`, `ResetToDefaultCommand`

**XAML Pattern:**
```xml
<ScrollViewer>
    <StackPanel Spacing="20" Margin="20" MaxWidth="800">
        <!-- Header -->
        <suki:GlassCard>
            <StackPanel Spacing="8">
                <TextBlock Text="Alert Configuration" FontSize="24" FontWeight="Bold" />
                <TextBlock Text="Configure how you receive alert notifications" />
            </StackPanel>
        </suki:GlassCard>
        
        <!-- Notification Channels -->
        <suki:GlassCard>
            <StackPanel Spacing="16">
                <TextBlock Text="Notification Channels" FontSize="18" FontWeight="SemiBold" />
                <Separator />
                
                <Grid ColumnDefinitions="*,Auto" RowDefinitions="Auto,Auto,Auto" RowSpacing="16">
                    <StackPanel Grid.Row="0" Grid.Column="0">
                        <TextBlock Text="Desktop Toast Notifications" FontWeight="SemiBold" />
                        <TextBlock Text="Show system notifications" Opacity="0.7" />
                    </StackPanel>
                    <ToggleSwitch Grid.Row="0" Grid.Column="1" IsChecked="{Binding DesktopToastEnabled}" />
                    
                    <!-- More channels -->
                </Grid>
            </StackPanel>
        </suki:GlassCard>
        
        <!-- Test Section -->
        <suki:GlassCard>
            <StackPanel Spacing="10">
                <TextBlock Text="Test Notifications" FontSize="18" FontWeight="SemiBold" />
                <Button Content="Send Test Alert" Command="{Binding TestNotificationCommand}" />
            </StackPanel>
        </suki:GlassCard>
    </StackPanel>
</ScrollViewer>
```

### 7. DataExportView

**Purpose:** Export historical sensor data to CSV format.

**Layout Structure:**
- Date range selection section
- Asset/sensor multi-select with checkboxes
- Export format options (CSV, JSON)
- Column selection for CSV export
- Progress bar for export operation
- Export history list

**SukiUI Components:**
- `GlassCard` for sections
- `DatePicker` for date range
- `ListBox` with checkboxes for multi-select
- `ProgressBar` for export progress
- `Button` for export action

**Data Binding:**
- ViewModel: `DataExportViewModel`
- Properties: `StartDate`, `EndDate`, `SelectedAssets`, `SelectedSensors`, `ExportFormat`, `IsExporting`, `ExportProgress`, `ExportHistory`
- Commands: `ExportCommand`, `CancelExportCommand`, `SelectAllAssetsCommand`, `ClearSelectionCommand`

**XAML Pattern:**
```xml
<Grid RowDefinitions="Auto,*,Auto" Spacing="20" Margin="20">
    <!-- Header -->
    <suki:GlassCard Grid.Row="0">
        <StackPanel Spacing="8">
            <TextBlock Text="Data Export" FontSize="24" FontWeight="Bold" />
            <TextBlock Text="Export historical sensor data for analysis" />
        </StackPanel>
    </suki:GlassCard>
    
    <!-- Export Configuration -->
    <Grid Grid.Row="1" ColumnDefinitions="*,*" ColumnSpacing="20">
        <!-- Date Range -->
        <suki:GlassCard Grid.Column="0">
            <StackPanel Spacing="16">
                <TextBlock Text="Date Range" FontSize="18" FontWeight="SemiBold" />
                <DatePicker SelectedDate="{Binding StartDate}" />
                <DatePicker SelectedDate="{Binding EndDate}" />
            </StackPanel>
        </suki:GlassCard>
        
        <!-- Asset/Sensor Selection -->
        <suki:GlassCard Grid.Column="1">
            <StackPanel Spacing="16">
                <TextBlock Text="Select Data" FontSize="18" FontWeight="SemiBold" />
                <ListBox ItemsSource="{Binding Assets}" SelectionMode="Multiple" />
            </StackPanel>
        </suki:GlassCard>
    </Grid>
    
    <!-- Export Actions -->
    <suki:GlassCard Grid.Row="2">
        <Grid ColumnDefinitions="*,Auto">
            <ProgressBar Grid.Column="0" Value="{Binding ExportProgress}" IsVisible="{Binding IsExporting}" />
            <StackPanel Grid.Column="1" Orientation="Horizontal" Spacing="10">
                <Button Content="Cancel" IsVisible="{Binding IsExporting}" />
                <Button Content="Export" Command="{Binding ExportCommand}" Classes="Accent" />
            </StackPanel>
        </Grid>
    </suki:GlassCard>
</Grid>
```

### 8. Enhanced SettingsView

**Purpose:** Comprehensive system settings including IoT-specific configurations.

**Enhancements to Existing View:**
- Add "Streaming Configuration" section
- Add "Data Retention" section
- Add "System Information" section with database stats

**New Sections:**
```xml
<!-- Streaming Configuration -->
<suki:GlassCard>
    <StackPanel Spacing="16">
        <TextBlock Text="Streaming Configuration" FontSize="18" FontWeight="SemiBold" />
        <Separator />
        
        <Grid ColumnDefinitions="*,Auto" RowDefinitions="Auto,Auto" RowSpacing="16">
            <StackPanel Grid.Row="0" Grid.Column="0">
                <TextBlock Text="Update Interval (ms)" FontWeight="SemiBold" />
                <TextBlock Text="How often to update real-time data" Opacity="0.7" />
            </StackPanel>
            <NumericUpDown Grid.Row="0" Grid.Column="1" Value="{Binding UpdateInterval}" Minimum="100" Maximum="5000" />
            
            <StackPanel Grid.Row="1" Grid.Column="0">
                <TextBlock Text="Buffer Size" FontWeight="SemiBold" />
                <TextBlock Text="Number of data points to keep in memory" Opacity="0.7" />
            </StackPanel>
            <NumericUpDown Grid.Row="1" Grid.Column="1" Value="{Binding BufferSize}" Minimum="100" Maximum="10000" />
        </Grid>
    </StackPanel>
</suki:GlassCard>

<!-- Data Retention -->
<suki:GlassCard>
    <StackPanel Spacing="16">
        <TextBlock Text="Data Retention" FontSize="18" FontWeight="SemiBold" />
        <Separator />
        
        <Grid ColumnDefinitions="*,Auto">
            <StackPanel Grid.Column="0">
                <TextBlock Text="Retention Period (days)" FontWeight="SemiBold" />
                <TextBlock Text="How long to keep historical data" Opacity="0.7" />
            </StackPanel>
            <NumericUpDown Grid.Column="1" Value="{Binding RetentionDays}" Minimum="7" Maximum="365" />
        </Grid>
    </StackPanel>
</suki:GlassCard>
```

## Data Models

### View-Specific Models

**AssetCardModel** (for MonitoringDashboardView)
```csharp
public class AssetCardModel
{
    public int AssetId { get; set; }
    public string Name { get; set; }
    public AssetStatus Status { get; set; }
    public int SensorCount { get; set; }
    public int ActiveAlerts { get; set; }
    public Dictionary<string, double> KeyMetrics { get; set; }
    public List<double> TrendData { get; set; } // For sparkline
}
```

**AlertFilterModel** (for AlertManagementView)
```csharp
public class AlertFilterModel
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? AssetId { get; set; }
    public int? SensorId { get; set; }
    public AlertSeverity? Severity { get; set; }
    public bool? IsAcknowledged { get; set; }
}
```

**ThresholdEditModel** (for ThresholdConfigurationView)
```csharp
public class ThresholdEditModel
{
    public int SensorId { get; set; }
    public string SensorName { get; set; }
    public SensorType Type { get; set; }
    public double? CurrentMinThreshold { get; set; }
    public double? CurrentMaxThreshold { get; set; }
    public double? NewMinThreshold { get; set; }
    public double? NewMaxThreshold { get; set; }
    public bool HasChanges { get; set; }
    public bool IsValid { get; set; }
    public string ValidationMessage { get; set; }
}
```

## Error Handling

### Validation Patterns

**Threshold Validation:**
- Min < Max validation with visual feedback (red border)
- Range validation based on sensor type
- Display validation messages below input controls

**Date Range Validation:**
- End date must be after start date
- Maximum range of 90 days for exports
- Display error message in GlassCard with warning icon

**Export Validation:**
- At least one asset/sensor must be selected
- Valid date range required
- Display validation summary before export

### Error Display Pattern

```xml
<Border Background="#FFEBEE" BorderBrush="#F44336" BorderThickness="1" 
        CornerRadius="8" Padding="12" IsVisible="{Binding HasError}">
    <StackPanel Orientation="Horizontal" Spacing="10">
        <i:Icon Value="fa-solid fa-circle-exclamation" Foreground="#F44336" />
        <TextBlock Text="{Binding ErrorMessage}" Foreground="#C62828" />
    </StackPanel>
</Border>
```

## Testing Strategy

### Visual Testing
- Test all views in both Light and Dark themes
- Verify responsive behavior at minimum width (1024px)
- Test with varying data volumes (empty, few items, many items)
- Verify all SukiUI components render correctly

### Interaction Testing
- Test all button commands
- Verify data binding updates
- Test navigation between views
- Verify real-time updates when streaming

### Cross-Platform Testing
- Test on Windows, Linux, and macOS
- Verify font rendering
- Test icon display
- Verify theme switching

## Performance Considerations

### Real-Time Updates
- Use Rx.NET throttling (500ms) for chart updates
- Implement virtual scrolling for large DataGrids (>100 rows)
- Debounce search/filter inputs (300ms)

### Memory Management
- Dispose of subscriptions in ViewModel cleanup
- Limit chart data points to visible window
- Use weak event patterns for long-lived subscriptions

### Rendering Optimization
- Use `IsVisible` binding instead of removing elements
- Minimize nested layouts
- Use `ItemsControl` virtualization for large lists

## Implementation Notes

### Naming Conventions
- Views: `[Feature]View.axaml`
- ViewModels: `[Feature]ViewModel.cs`
- Commands: `[Action]Command`
- Properties: PascalCase

### File Organization
```
AquaPP/
├── Views/
│   └── Pages/
│       ├── MonitoringDashboardView.axaml
│       └── MonitoringDashboardView.axaml.cs
├── ViewModels/
│   └── Pages/
│       └── MonitoringDashboardViewModel.cs
└── Converters/
    └── [Custom converters as needed]
```

### Dependency Injection
All ViewModels will be registered in `App.axaml.cs`:
```csharp
services.AddTransient<MonitoringDashboardViewModel>();
services.AddTransient<SensorDetailViewModel>();
services.AddTransient<AlertManagementViewModel>();
// etc.
```

### Navigation Integration
Views will be added to the `MainWindowViewModel.Pages` collection:
```csharp
Pages = new ObservableCollection<PageViewModel>
{
    new PageViewModel("Dashboard", "fa-solid fa-gauge", new MonitoringDashboardView()),
    new PageViewModel("Sensors", "fa-solid fa-sensor", new SensorListView()),
    new PageViewModel("Alerts", "fa-solid fa-bell", new AlertManagementView()),
    // etc.
};
```
