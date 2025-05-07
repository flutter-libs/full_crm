import 'package:flutter/material.dart';
import 'package:frontend/models/campaign.dart';

class CampaignUpdateScreen extends StatefulWidget {
  final int? campaignId;
  const CampaignUpdateScreen({super.key, Campaign? campaign, this.campaignId});
  static const String id = "campaign_update_screen";
  @override
  State<CampaignUpdateScreen> createState() => _CampaignUpdateScreenState();
}

class _CampaignUpdateScreenState extends State<CampaignUpdateScreen> {
  @override
  Widget build(BuildContext context) {
    return const Placeholder();
  }
}
