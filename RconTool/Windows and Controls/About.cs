using System;
using System.Windows.Forms;

namespace RconTool
{
    public partial class About : Form
    {
        public About()
        {

            InitializeComponent();

            labelVersion.Text = "Version: " + App.toolversion;

            linkLabelProjectGitHub.Links.Add(0, "http://www.github.com/BIRD-COMMAND".Length, "http://www.github.com/BIRD-COMMAND");
            linkLabelProjectGitHub.LinkClicked += (o, e) =>
            {
                linkLabelProjectGitHub.LinkVisited = true;
                System.Diagnostics.Process.Start("http://www.github.com/BIRD-COMMAND");
            };

            linkLabelJoinTheDiscord.Links.Add(0, "http://discord.gg/upuphgd".Length, "http://discord.gg/upuphgd");
            linkLabelJoinTheDiscord.LinkClicked += (o, e) =>
            {
                linkLabelJoinTheDiscord.LinkVisited = true;
                System.Diagnostics.Process.Start("http://discord.gg/upuphgd");
            };

        }

    }
}
