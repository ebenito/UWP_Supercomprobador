using System;
using System.Text.RegularExpressions;
using Microsoft.UI.Xaml;
using SuperComrprobador_UWP.Helpers;

namespace SuperComrprobador_UWP.ViewModels
{
    public class P1NIFViewModel : Observable
    {
        private static readonly string _letrasNif = "TRWAGMYFPDXBNJZSQVHLCKE";

        private string _nif = string.Empty;
        public string Nif
        {
            get => _nif;
            set
            {
                Set(ref _nif, value?.ToUpper() ?? string.Empty);
                Validar();
            }
        }

        private string _resultado = string.Empty;
        public string Resultado
        {
            get => _resultado;
            private set => Set(ref _resultado, value);
        }

        private bool _esValido;
        public bool EsValido
        {
            get => _esValido;
            private set => Set(ref _esValido, value);
        }

        private bool _hayResultado;
        public bool HayResultado
        {
            get => _hayResultado;
            private set
            {
                Set(ref _hayResultado, value);
                OnPropertyChanged(nameof(ResultadoVisibility));
            }
        }

        public Visibility ResultadoVisibility => _hayResultado ? Visibility.Visible : Visibility.Collapsed;

        private void Validar()
        {
            var nif = _nif.Trim();
            if (string.IsNullOrEmpty(nif))
            {
                HayResultado = false;
                return;
            }

            HayResultado = true;

            if (Regex.IsMatch(nif, @"^\d{8}[A-Z]$"))
            {
                // NIF estándar
                int numero = int.Parse(nif.Substring(0, 8));
                char letraEsperada = _letrasNif[numero % 23];
                char letraIntroducida = nif[8];
                EsValido = letraIntroducida == letraEsperada;
                Resultado = EsValido
                    ? $"✔ NIF válido. Letra correcta: {letraEsperada}"
                    : $"✘ NIF inválido. La letra correcta es '{letraEsperada}', no '{letraIntroducida}'";
            }
            else if (Regex.IsMatch(nif, @"^[XYZ]\d{7}[A-Z]$"))
            {
                // NIE
                string nieNum = nif.Replace("X", "0").Replace("Y", "1").Replace("Z", "2");
                int numero = int.Parse(nieNum.Substring(0, 8));
                char letraEsperada = _letrasNif[numero % 23];
                char letraIntroducida = nif[8];
                EsValido = letraIntroducida == letraEsperada;
                Resultado = EsValido
                    ? $"✔ NIE válido. Letra correcta: {letraEsperada}"
                    : $"✘ NIE inválido. La letra correcta es '{letraEsperada}', no '{letraIntroducida}'";
            }
            else
            {
                EsValido = false;
                Resultado = "✘ Formato incorrecto. Introduce 8 dígitos y letra (NIF) o X/Y/Z + 7 dígitos + letra (NIE)";
            }
        }
    }
}
