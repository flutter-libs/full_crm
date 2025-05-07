import 'package:flutter/material.dart';
import 'package:frontend/models/campaign.dart';
import 'package:frontend/services/campaign_api_service.dart';
import 'package:frontend/screens/campaign/campaign_update_screen.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;

class CampaignDetailScreen extends StatefulWidget {
  final int? campaignId;
  final Campaign? campaign;

  const CampaignDetailScreen({super.key, this.campaignId, this.campaign});
  static const String id = "campaign_detail_screen";
  @override
  State<CampaignDetailScreen> createState() => _CampaignDetailScreenState();
}

class _CampaignDetailScreenState extends State<CampaignDetailScreen> {
  final CampaignApiService _apiService = CampaignApiService();

  void updateCampaign() async {
    final updatedCampaign = await Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => CampaignUpdateScreen(campaign: widget.campaign),
      ),
    );

    if (updatedCampaign != null) {
      setState(() {
        // update UI with new campaign data
        widget.campaign!.name = updatedCampaign.name;
        widget.campaign!.description = updatedCampaign.description;
        widget.campaign!.actualCost = updatedCampaign.actualCost;
        widget.campaign!.actualResponses = updatedCampaign.actualResponses;
        widget.campaign!.budget = updatedCampaign.budget;
        widget.campaign!.actualSales = updatedCampaign.actualSales;
        widget.campaign!.startDate = updatedCampaign.startDate;
        widget.campaign!.endDate = updatedCampaign.endDate;
        widget.campaign!.expectedResponses = updatedCampaign.expectedResponses;
        widget.campaign!.expectedSales = updatedCampaign.expectedSales;
        widget.campaign!.status = updatedCampaign.status;
        widget.campaign!.type = updatedCampaign.type;
      });
      alert.showSuccessToast(context, 'Campaign Updated Successfully', 'Campaign Updated');
    }
  }

  void deleteCampaign() async {
    final confirm = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Confirm Deletion'),
        content: const Text('Are you sure you want to delete this campaign?'),
        actions: [
          TextButton(onPressed: () => Navigator.pop(context, false), child: const Text('Cancel')),
          TextButton(onPressed: () => Navigator.pop(context, true), child: const Text('Delete')),
        ],
      ),
    );

    if (confirm == true) {
      bool success = await _apiService.deleteCampaign(widget.campaign!.id!) as bool;
      if (success) {
        if (context.mounted) {
          Navigator.pop(context); // Go back after delete
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(content: Text('Campaign deleted successfully')),
          );
        }
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Failed to delete campaign')),
        );
      }
    }
  }

  String _formatDate(DateTime? date) {
    return date != null ? "${date.toLocal()}".split(' ')[0] : "N/A";
  }

  Widget _buildDetail(String title, String? value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 6),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            "$title: ",
            style: const TextStyle(fontWeight: FontWeight.bold),
          ),
          Expanded(child: Text(value ?? "N/A")),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final campaign = widget.campaign;

    return Scaffold(
      appBar: CustomAppBar(
        title: campaign!.name ?? 'Campaign Details',
        actions: [
          IconButton(
            icon: const Icon(Icons.edit),
            onPressed: updateCampaign,
          ),
          IconButton(
            icon: const Icon(Icons.delete),
            onPressed: deleteCampaign,
          ),
        ],
      ),
      drawer: SideNavDrawer(),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            _buildDetail("Name", campaign.name),
            _buildDetail("Description", campaign.description),
            _buildDetail("Type", campaign.type),
            _buildDetail("Status", campaign.status),
            _buildDetail("Start Date", _formatDate(campaign.startDate)),
            _buildDetail("End Date", _formatDate(campaign.endDate)),
            _buildDetail("Budget", campaign.budget?.toStringAsFixed(2)),
            _buildDetail("Actual Cost", campaign.actualCost?.toStringAsFixed(2)),
            _buildDetail("Expected Responses", campaign.expectedResponses?.toString()),
            _buildDetail("Actual Responses", campaign.actualResponses?.toString()),
            _buildDetail("Expected Sales", campaign.expectedSales?.toStringAsFixed(2)),
            _buildDetail("Actual Sales", campaign.actualSales?.toStringAsFixed(2)),
            _buildDetail("Date Created", _formatDate(campaign.dateCreated)),
            _buildDetail("Date Updated", _formatDate(campaign.dateUpdated)),
            _buildDetail("Created By", campaign.createdByUser?.name ?? campaign.createdByUserId),
          ],
        ),
      ),
    );
  }
}