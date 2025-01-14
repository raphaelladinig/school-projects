{
  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
  };

  outputs =
    {
      self,
      nixpkgs,
      ...
    }:
    let
      systems = [
        "x86_64-linux"
      ];

      forEachSystem = f: nixpkgs.lib.genAttrs systems (system: f pkgsFor.${system});

      pkgsFor = nixpkgs.lib.genAttrs systems (
        system:
        import nixpkgs {
          inherit system;
          config.allowUnfree = true;
          overlays = [
            self.overlays.additions
            self.overlays.modifications
          ];
        }
      );
    in
    {
      formatter = forEachSystem (pkgs: pkgs.nixfmt-rfc-style);

      devShells = forEachSystem (pkgs: import ./shell.nix { inherit pkgs; });
    };
}
