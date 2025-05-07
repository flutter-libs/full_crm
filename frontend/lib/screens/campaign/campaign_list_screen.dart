import 'package:flutter/material.dart';
import 'package:frontend/models/campaign.dart';
import 'package:frontend/services/campaign_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;

class CampaignListScreen extends StatefulWidget {
  const CampaignListScreen({super.key});

  static const String id = "campaign_list_screen";

  @override
  State<CampaignListScreen> createState() => _CampaignListScreenState();
}

class _CampaignListScreenState extends State<CampaignListScreen> {
  final CampaignApiService _apiService = CampaignApiService();
  List<Campaign> _campaigns = [];
  bool _isLoading = true;
  @override
  void initState() {
    super.initState();
    _fetchCampaigns();
  }
  Future<void> _fetchCampaigns() async {
    try {
      List<Campaign> campaigns = await _apiService.getAllCampaigns();
      setState(() {
        _campaigns = campaigns;
        _isLoading = false;
      });
    } catch (e) {
      alert.showErrorToast(context, 'Failed to fetch campaigns', 'Error Fetching Campaigns');
      setState(() {
        _isLoading = false;
      });
    }
  }
  Widget _buildCampaignCard(Campaign campaign) {
    return Card(
      elevation: 2,
      margin: const EdgeInsets.symmetric(vertical: 8, horizontal: 16),
      child: ListTile(
        title: Text(campaign.name ?? 'No Name'),
        subtitle: Text(campaign.description ?? 'No Description'),
        trailing: Text(campaign.status ?? 'Unknown'),
        onTap: () {
          // You can navigate to a detail page here
        },
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: CustomAppBar(title: 'Campaigns', showBackButton: false),
      drawer: SideNavDrawer(),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _campaigns.isEmpty
          ? const Center(child: Text('No campaigns found.'))
          : ListView.builder(
        itemCount: _campaigns.length,
        itemBuilder: (context, index) {
          return _buildCampaignCard(_campaigns[index]);
        },
      ),
    );
  }
}
