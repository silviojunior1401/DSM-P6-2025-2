import joblib
import numpy as np
import pandas as pd
import argparse
import json
import sys

def predict_heart_disease(input_data):
    # Carregar o modelo
    loaded_model = joblib.load('./src/utils/python/heart_modelo_lda.joblib')
    
    # Converter a entrada para um DataFrame
    novos_df = pd.DataFrame(input_data, columns=[
        'age', 'resting bp s', 'cholesterol', 'max heart rate', 
        'oldpeak', 'sex', 'chest pain type', 'fasting blood sugar', 
        'resting ecg', 'exercise angina', 'ST slope'
    ])
    
    # Define as colunas para normalização
    cols_to_normalize = ['age', 'resting bp s', 'cholesterol', 'max heart rate', 'oldpeak']
    
    # Separa as colunas a serem normalizadas e as demais
    novos_norm_subset = novos_df[cols_to_normalize].values
    novos_other_subset = novos_df.drop(columns=cols_to_normalize).values
    
    # Valores de média e desvio padrão do conjunto de treinamento
    mu = [53.51089325, 132.39651416, 198.79956427, 136.80936819, 0.88736383]
    sigma = [9.43261651, 18.51415412, 109.38414455, 25.46033414, 1.06657015]
    
    # Aplicar a normalização
    novos_scaled_subset = (novos_norm_subset - mu) / sigma
    
    # Combinar os dados normalizados com as colunas não normalizadas
    novos_scaled = np.hstack((novos_scaled_subset, novos_other_subset))
    
    # Fazer a previsão
    previsao = loaded_model.predict(novos_scaled)
    
    return previsao

if __name__ == "__main__":
    # Configurar o parser de argumentos
    parser = argparse.ArgumentParser(description='Predição de doença cardíaca usando modelo LDA')
    parser.add_argument('--input', type=str, required=True, 
                        help='Dados de entrada em formato JSON. Deve ser uma lista de listas.')
    
    # Analisar os argumentos
    args = parser.parse_args()
    
    try:
        # Converter a string JSON para uma lista de listas
        input_data = json.loads(args.input)
        
        # Verificar se os dados estão no formato correto
        if not isinstance(input_data, list) or not all(isinstance(item, list) for item in input_data):
            raise ValueError("Os dados de entrada devem ser uma lista de listas")
        
        # Fazer a previsão
        resultado = predict_heart_disease(input_data)
        
        # Imprimir o resultado
        print("Resultado da previsão:")
        print(resultado)
        
    except json.JSONDecodeError:
        print("Erro: O formato JSON fornecido é inválido.", file=sys.stderr)
    except ValueError as e:
        print(f"Erro: {str(e)}", file=sys.stderr)
    except Exception as e:
        print(f"Erro inesperado: {str(e)}", file=sys.stderr)