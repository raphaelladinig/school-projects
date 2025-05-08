{
  imports = [
    ./ssh.nix
  ];

  users.users.root = {
    hashedPassword = "";
    openssh.authorizedKeys.keyFiles = [
      ./id_raphael.pub
    ];
  };
}
