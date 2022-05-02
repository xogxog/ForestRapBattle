from rest_framework import serializers
from django.contrib.auth import get_user_model
from game.models import ProfileImage,Match

User = get_user_model()



class ProfileImageSerializer(serializers.ModelSerializer) :
  
  class Meta :
    model = ProfileImage
    fields = '__all__'

# 유저 생성 시
class UserSerializer(serializers.ModelSerializer) :

  password = serializers.CharField(write_only = True)
  profile = ProfileImageSerializer(read_only = True)

  class Meta :
    model = User
    fields = ('email','nickname','profile','password')


# 유저 프로필 조회
class UserInfoSerializer(serializers.ModelSerializer) :
  # 전적 
  class MatchListSerializer(serializers.ModelSerializer) :
    class Meta :
      model = Match
      fields = ('match_id','date','winner_user_id','loser_user_id')

  user_match = MatchListSerializer(many=True, read_only = True)
  profile = ProfileImageSerializer(read_only = True)

  class Meta :
    model = User
    fields = ('user_id','email','profile','nickname','user_match')