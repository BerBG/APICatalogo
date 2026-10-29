#!/usr/bin/env bash
# Git Bash / MSYS2 no Windows: abre APICatalogo.sln no Visual Studio 2022.
# Uso (na pasta do repo): ./scripts/abrir-visual-studio.sh

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
SLN="$REPO_ROOT/APICatalogo.sln"

if [[ ! -f "$SLN" ]]; then
  echo "Não encontrei APICatalogo.sln em $REPO_ROOT"
  exit 1
fi

VSWHERE="/c/Program Files (x86)/Microsoft Visual Studio/Installer/vswhere.exe"
if [[ -f "$VSWHERE" ]]; then
  INSTALL_PATH="$("$VSWHERE" -latest -products '*' -requires Microsoft.ComponentGroup.CoreEditor -property installationPath)"
  DEVENV="$INSTALL_PATH/Common7/IDE/devenv.exe"
  if [[ -f "$DEVENV" ]]; then
    echo "Abrindo: $SLN"
    "$DEVENV" "$SLN" &
    exit 0
  fi
fi

# Fallback: associação do Windows com o .sln
if command -v cmd.exe >/dev/null 2>&1; then
  WIN_SLN="$(cygpath -w "$SLN" 2>/dev/null || echo "$SLN")"
  cmd.exe //c start "" "$WIN_SLN"
  exit 0
fi

echo "Visual Studio não encontrado. Abra manualmente: $SLN"
exit 1
