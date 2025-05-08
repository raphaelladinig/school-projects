{
  description = "nix-config";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
    nixpkgs-stable.url = "github:nixos/nixpkgs/nixos-24.11";
    nixpkgs-unstable.url = "github:NixOS/nixpkgs/nixos-unstable";

    disko = {
      url = "github:nix-community/disko";
      inputs.nixpkgs.follows = "nixpkgs";
    };
  };

  outputs = {
    self,
    nixpkgs,
    ...
  } @ inputs: let
    inherit (self) outputs;

    systems = [
      "x86_64-linux"
    ];

    forEachSystem = f: nixpkgs.lib.genAttrs systems (system: f pkgsFor.${system});

    pkgsFor = nixpkgs.lib.genAttrs systems (
      system:
        import nixpkgs {
          inherit system;
          overlays = builtins.attrValues self.overlays;
          config.allowUnfree = true;
        }
    );
  in {
    devShells = forEachSystem (
      pkgs: {
        default = pkgs.mkShell {
          nativeBuildInputs = with pkgs; [
            nixos-anywhere
          ];
        };
      }
    );

    overlays = import ./overlays {inherit inputs;};

    nixosConfigurations = builtins.listToAttrs (
      map (host: {
        name = host;
        value = nixpkgs.lib.nixosSystem {
          specialArgs = {
            inherit inputs outputs;
          };
          modules = [
            ./hosts/nixos/${host}
            ./modules/nixos/host-spec.nix
          ];
        };
      }) (builtins.attrNames (builtins.readDir ./hosts/nixos))
    );
  };
}
