{pkgs ? import <nixpkgs> {}}: {
  default = pkgs.mkShell {
    nativeBuildInputs = with pkgs; [
      (pkgs.python3.withPackages (python-pkgs:
        with python-pkgs; [
          pillow
          tensorflow
          numpy
        ]))
    ];
  };
}
