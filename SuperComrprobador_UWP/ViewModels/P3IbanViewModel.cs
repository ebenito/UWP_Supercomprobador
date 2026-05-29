using System;
using Microsoft.UI.Xaml;
using SuperComrprobador_UWP.Helpers;
using IbanLibraryPCL;

namespace SuperComrprobador_UWP.ViewModels
{
    public class P3IbanViewModel : Observable
    {
        private string _iban = string.Empty;
        public string Iban
        {
            get => _iban;
            set
            {
                Set(ref _iban, value ?? string.Empty);
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
            if (string.IsNullOrWhiteSpace(_iban))
            {
                HayResultado = false;
                return;
            }

            HayResultado = true;
            StatusData status = IbanLibraryPCL.Iban.CheckIban(_iban, true);
            EsValido = status.IsValid;
            Resultado = EsValido ? $"✔ {status.Message}" : $"✘ {status.Message}";
        }
    }
}
